using System;
using System.Collections.Generic;
using ElasticImporter.Model;
using Nest;
using Serilog;

namespace ElasticImporter.Steps
{
    public class ImportStep: IAbstractStep
    {
        private const int BatchSize = 500;

        private readonly ElasticClient _elasticClient;
        
        public ImportStep()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("movies");

            _elasticClient = new ElasticClient(settings);
        }
        
        public void Execute()
        {

            var movies = MovieRepository.Instance.FindAll();

            var batchNum = 0;

            var batch = new List<Movie>();

            foreach (var movie in movies)
            {
                batch.Add(movie);
                if (batch.Count != BatchSize) continue;
                IndexBulk(batch);
                Log.Information($"indexing batch {batchNum++}");
                batch.Clear();
            }
            Log.Information($"indexing last batch, items: {batch.Count}");
            IndexBulk(batch);
        }

        public string Name()
        {
            return "ImportStep";
        }

        private void IndexBulk(IEnumerable<Movie> movies)
        {
            var indexManyResponse = _elasticClient.IndexMany(movies);

            if (!indexManyResponse.Errors) return;
            foreach (var itemWithError in indexManyResponse.ItemsWithErrors) 
            {
                Log.Error("Failed to index document {0}: {1}", itemWithError.Id, itemWithError.Error);
            }
        }
    }
}