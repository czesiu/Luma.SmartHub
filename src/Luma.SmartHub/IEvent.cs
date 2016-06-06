using System.Threading.Tasks;

namespace Luma.SmartHub
{
    public interface IEvent
    {
        Task Raise(IEventArgs args);
    }
}