using CQRSlite.Messages;

namespace CQRSlite.Events
{
    public interface IAsyncEventHandler<in T> : IAsyncHandler<T> where T : IEvent
    {
    }
}