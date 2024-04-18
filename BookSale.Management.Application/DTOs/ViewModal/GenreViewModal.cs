using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Application.DTOs.ViewModal
{
    public class GenreViewModal
    {
        public int? Id { get; set; } = 0;

        [Required(ErrorMessage = "Genre name must be not emty.")]
        public string Name { get; set; }
    }
}
