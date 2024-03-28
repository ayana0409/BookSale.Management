using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managament.Domain.Entities
{
    public class CartDetail : BaseEntity
    {
        public double Price { get; set; }
        public int Quantity { get; set; }
        [StringLength(500)]
        public string? Note { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public int CartId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book? Book { get; set; }
        [ForeignKey(nameof(CartId))]
        public Cart? Cart { get; set; }
    }
}
