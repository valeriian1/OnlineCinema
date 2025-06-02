using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string MovieCast { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string PosterUrl { get; set; }
        public IList<Session> Sessions { get; set; }
    }
}
