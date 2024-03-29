using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Actor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public virtual ICollection<ActorFilm> ActorFilm { get; set; }
        public bool IsActive { get; set; } = true;
        
    }
}