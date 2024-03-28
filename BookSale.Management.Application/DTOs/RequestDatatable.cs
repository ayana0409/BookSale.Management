using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
