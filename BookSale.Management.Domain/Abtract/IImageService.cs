using Microsoft.AspNetCore.Http;

namespace BookSale.Management.Application.Services
{
    public interface IImageService
    {
        Task<bool> SaveImage(List<IFormFile> images, string path, string? defaultName);
    }
}