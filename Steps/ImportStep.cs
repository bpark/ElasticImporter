using System;
using ElasticImporter.Model;
using Nest;

namespace ElasticImporter.Steps
{
    public class ImportStep: IAbstractStep
    {
        public void Execute()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("movies");

            var client = new ElasticClient(settings);

            var movies = MovieRepository.Instance.FindAll();
            
            foreach (var movie in movies)
            {
                var indexResponse = client.IndexDocument(movie);          
            }

        }

        public string Name()
        {
            return "ImportStep";
        }
    }
}