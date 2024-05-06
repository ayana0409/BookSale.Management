using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Managament.Domain.Entities
{
    public class Cart : BaseEntity
    {
        [StringLength(500)]
        public string? Code { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        [StringLength(500)]
        public string? Note { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}
