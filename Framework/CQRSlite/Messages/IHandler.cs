using System.Threading.Tasks;

namespace CQRSlite.Messages
{
    public interface IHandler<in T> where T : IMessage
    {
        void Handle(T message);
    }

    public interface IAsyncHandler<in T> where T : IMessage
    {
        Task HandleAsync(T message);
    }
}