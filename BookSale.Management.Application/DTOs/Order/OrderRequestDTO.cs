using BookSale.Managament.Domain.Enums;
using BookSale.Management.Application.DTOs.Books;

namespace BookSale.Management.Application.DTOs.Order
{
    public class OrderRequestDTO
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedOn { get; set; }
        public double TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string UserId { get; set; }
        public StatusProcessing Status { get; set; }
        public List<BookCartDTO> Books { get; set; }
    }
}
