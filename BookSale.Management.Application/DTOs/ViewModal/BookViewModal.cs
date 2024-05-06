using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Application.DTOs.ViewModal
{
    public class BookViewModal
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Genre must be not empty.")]
        [DisplayName("Genre Name")]
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Code must be not empty.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Title must be not empty.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Available must be not empty.")]
        public int Available { get; set; }
        [Required(ErrorMessage = "Price must be not empty.")]
        public double Price { get; set; }
        public string? Publisher { get; set; }
        [Required(ErrorMessage = "Author must be not empty.")]
        public string Author { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }

        public bool IsActive { get; set; }
    }
}
