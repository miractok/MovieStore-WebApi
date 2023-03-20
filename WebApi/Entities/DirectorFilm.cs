using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class DirectorFilm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public bool IsActive { get; set; } = true;
        
    }
}