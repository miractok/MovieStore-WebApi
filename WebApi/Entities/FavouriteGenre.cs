using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class FavouriteGenre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public bool IsActive { get; set; } = true;
    }
}