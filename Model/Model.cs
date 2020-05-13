using System.Collections.Generic;
using System.Linq;

namespace ElasticImporter.Model
{
    public class Movie
    {
        public int MovieId { get; private set; }
        public string Title { get; private set; }
        public List<string> Genres { get; private set; }

        public static Movie CreateFromLine(string line)
        {
            var elements = line.Split(",");
            var movie = new Movie();
            if (int.TryParse(elements[0], out var id))
            {
                movie.MovieId = id;
            }

            movie.Title = elements[1];
            movie.Genres = elements[2].Split("|").ToList();
            return movie;
        }

        public override string ToString()
        {
            return $"{nameof(MovieId)}: {MovieId}, {nameof(Title)}: {Title}, {nameof(Genres)}: {string.Join(", ", Genres)}";
        }
    }
}