using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public int Price { get; set; }
        public DateTime PurchaseDate { get; set; }

        public bool IsActive { get; set; } = true;
        
    }
}