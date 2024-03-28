using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.UI.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Username must be not empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password must be not empty")]
        [MinLength(3, ErrorMessage = "Password must be greater than 3 characters")]
        public string Password { get; set; }
        public bool HasRemember { get; set; }
    }
}
