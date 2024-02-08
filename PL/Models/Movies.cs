using Newtonsoft.Json.Linq;

namespace PL.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Poster { get; set; }
        public bool respuesta { get; set; }

        public JArray GenreIds { get; set; }

        public List<object> ListMovies { get; set; }
    }
}
