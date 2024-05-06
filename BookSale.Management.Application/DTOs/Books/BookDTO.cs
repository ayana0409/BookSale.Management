namespace BookSale.Management.Application.DTOs.Books
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Available { get; set; }
        public double Price { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string GenreName { get; set; }
    }
}
