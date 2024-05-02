using System.ComponentModel.DataAnnotations;

namespace BookSale.Managament.Domain.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
