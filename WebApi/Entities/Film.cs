using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Film
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public virtual ICollection<DirectorFilm> DirectorFilm { get; set; }
        public virtual ICollection<ActorFilm> ActorFilm { get; set; }
        public double Price { get; set; }
    }
}