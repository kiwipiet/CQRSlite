using CQRSlite.Messages;

namespace CQRSlite.Commands
{
    public interface IAsyncCommandHandler<in T> : IAsyncHandler<T> where T : ICommand
    {
    }
}