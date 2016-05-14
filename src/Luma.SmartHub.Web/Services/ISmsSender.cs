using System.Threading.Tasks;

namespace Luma.SmartHub.Web.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
