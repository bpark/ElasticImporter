using System.Collections.Generic;

namespace ElasticImporter.Model
{
    public class MovieRepository
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static MovieRepository()
        {
        }

        private MovieRepository()
        {
        }

        public static MovieRepository Instance { get; } = new MovieRepository();

        private readonly List<Movie> _movies = new List<Movie>();

        public void Add(Movie movie)
        {
            this._movies.Add(movie);
        }

        public IReadOnlyList<Movie> FindAll()
        {
            return _movies;
        }
    }
}