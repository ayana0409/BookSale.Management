using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Managament.Domain.Entities
{
    public class Cart : BaseEntity
    {
        [Required]
        [StringLength(500)]
        public string Code { get; set; }
        [StringLength(500)]
        public string? Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public short Status { get; set; }
    }
}
