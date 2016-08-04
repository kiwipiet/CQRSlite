using System;
using System.Threading.Tasks;
using CQRSlite.Messages;

namespace CQRSlite.Bus
{
    public interface IAsyncHandlerRegistrar
    {
        void RegisterHandler<T>(Func<T, Task> handler) where T : IMessage;
    }
}