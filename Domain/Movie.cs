using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string MovieCast { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Genre { get; set; }
        public string Description { get; set; }
        public string PosterUrl { get; set; }
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}



