
namespace BookSale.Management.Infrastructure.Services
{
    public interface IExcelHandler
    {
        Task<byte[]> Export<T>(List<T> dataItems) where T : class, new();
    }
}