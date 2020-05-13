using System;
using System.IO;
using ElasticImporter.Model;

namespace ElasticImporter.Steps
{
    public class DataReadStep : IAbstractStep
    {
        public void Execute()
        {
            using var reader = new StreamReader(@"C:\cygwin64\home\kurts\Entwicklung\projects\dotnet\Data\movies.csv");
            string line;
            while((line = reader.ReadLine()) != null)
            {
                var movie = Movie.CreateFromLine(line);
                //Console.WriteLine($"{movie}");
                
                MovieRepository.Instance.Add(movie);
            }
        }

        public string Name()
        {
            return "DataReadStep";
        }
    }
}