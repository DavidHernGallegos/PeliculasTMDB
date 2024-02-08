namespace PL.Models
{
    public class FavoritasModel
    {
        public bool Adult { get; set; }
        public string BackdropPath { get; set; }
        public List<object> GenereIds { get; set; }
        public int Id { get; set; }
        public string OriginalLenguaje { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public int Popularity { get; set; }
        public string PosterPath { get; set; }
        public string RelaseDate { get; set; }
        public string Title { get; set;}
        public bool Video { get; set; }
        public int VoteAvergae { get; set; }
        public int VoteCount { get; set; }
    }
}


