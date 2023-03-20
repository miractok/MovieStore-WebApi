using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class ActorFilm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public bool IsActive { get; set; } = true;
        
    }
}