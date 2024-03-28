using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managament.Domain.Entities
{
    public class Catalogue : BaseEntity
    {
        [Required]
        [StringLength(500)]
        public string? Title { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
