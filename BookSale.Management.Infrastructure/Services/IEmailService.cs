using BookSale.Managament.Domain.Setting;

namespace BookSale.Management.Infrastructure.Services
{
    public interface IEmailService
    {
        Task<bool> Send(EmailSetting emailSetting);
    }
}