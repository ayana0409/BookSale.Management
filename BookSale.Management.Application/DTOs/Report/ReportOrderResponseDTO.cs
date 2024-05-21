using BookSale.Managament.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.DTOs.Report
{
    public class ReportOrderResponseDTO
    {
        public string Code { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CustomerName { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        public StatusProcessing Status {  get; set; }

    }
}
