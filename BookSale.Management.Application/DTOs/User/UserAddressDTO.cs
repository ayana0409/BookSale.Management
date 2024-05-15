using BookSale.Managament.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.DTOs.User
{
    public class UserAddressDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Full name must be not empty.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Phone number must be not empty.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email must be not empty.")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address must be not empty.")]
        public string Address { get; set; }
        public string OrderId {  get; set; } 
        public PaymentMethod PaymentMethod { get; set; }
        public string? UserId { get; set; }
    }
}
