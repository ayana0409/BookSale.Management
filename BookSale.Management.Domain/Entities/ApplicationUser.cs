using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Managament.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(500)]
        public string? FullName { get; set; }
        [StringLength(1000)]
        public string? Address { get; set; }
        public string? MobilePhone { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
