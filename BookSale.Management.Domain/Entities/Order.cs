using BookSale.Managament.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Managament.Domain.Entities
{
    public class Order
    {
        [Required]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Code { get; set; }
        public DateTime CreatedOn { get; set; }
        public double TotalAmount { get; set; } 
        public PaymentMethod PaymentMethod { get; set; }
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public UserAddress Address { get; set; }
        public StatusProcessing Status { get; set; }

        public ICollection<OrderDetail> Details { get; set; }
    }
}
