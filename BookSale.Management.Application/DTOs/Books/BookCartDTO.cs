namespace BookSale.Management.Application.DTOs.Books
{
    public class BookCartDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
        public double Price { get; set; }

    }
}
