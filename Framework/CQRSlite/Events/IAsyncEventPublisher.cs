using System.Threading.Tasks;

namespace CQRSlite.Events
{
    public interface IAsyncEventPublisher
    {
        Task PublishAsync<T>(T @event) where T : IEvent;
    }
}