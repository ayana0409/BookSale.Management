using BookSale.Management.Application.DTOs.Books;

namespace BookSale.Management.UI.Models
{
    public class BookForSiteDTO
    {
        public int TotalRecord { get; set; }
        public int CurrentRecord { get; set; }
        public bool IsDisable { get; set; }
        public IEnumerable<BookDTO> Books { get; set; }
        public double ProressingValue { get; set; }
    }
}
