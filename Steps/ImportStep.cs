using System;
using System.Collections.Generic;
using System.Linq;
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

            while (batchNum * BatchSize < MovieRepository.Instance.Count())
            {
                Log.Information($"indexing batch {batchNum}");
                movies.Skip(BatchSize * batchNum);
                var batch = movies.Take(BatchSize);
                IndexBulk(batch);
                batchNum++;
            }
            
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