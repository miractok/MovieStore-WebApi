using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class CustomerToDo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Orders { get; set; }
        public bool IsActive { get; set; } = true;
        
    }
}