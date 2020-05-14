using System;
using System.IO;
using ElasticImporter.Model;
using Serilog;

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
                
                MovieRepository.Instance.Add(movie);
            }
            Log.Information($"repository contains {MovieRepository.Instance.Count()}");
        }

        public string Name()
        {
            return "DataReadStep";
        }
    }
}