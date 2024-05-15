using BookSale.Managament.Domain.Enums;
using BookSale.Management.Application.DTOs.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.DTOs.Cart
{
    public class CartRequestDTO
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public StatusProcessing Status { get; set; }
        public List<BookCartDTO> Books { get; set; }
    }
}
