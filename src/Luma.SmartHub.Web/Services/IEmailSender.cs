using System.Threading.Tasks;

namespace Luma.SmartHub.Web.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
