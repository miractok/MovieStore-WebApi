using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Director
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public bool IsActive { get; set; } = true;
        
    }
}