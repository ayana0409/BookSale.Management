using System.ComponentModel.DataAnnotations;

namespace BookSale.Managament.Domain.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        [StringLength(500)]
        public string? Name { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
