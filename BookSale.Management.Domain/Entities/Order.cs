using BookSale.Managament.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Required, StringLength(50)]
        public string UserId { get; set; }
        public short Status { get; set; }
    }
}
