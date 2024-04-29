using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Available { get; set; } = 0;
        public double Price { get; set; } = 0;
        public string? Publisher { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string GenreName { get; set; }
    }
}
