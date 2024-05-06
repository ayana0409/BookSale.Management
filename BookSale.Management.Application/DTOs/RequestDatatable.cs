using Microsoft.AspNetCore.Mvc;

namespace BookSale.Management.Application.DTOs
{
    public class RequestDatatable
    {
        [BindProperty(Name = "length")]
        public int PageSize { get; set; }
        [BindProperty(Name = "start")]
        public int SkipItems { get; set; }
        [BindProperty(Name = "search[value]")]
        public string? Keyword { get; set; }
        public int Draw { get; set; }
        [BindProperty(Name = "order[0][column]")]
        public int? OrderColunm { get; set; }
        [BindProperty(Name = "order[0][dir]")]
        public string? OrderType { get; set; }
    }
}
