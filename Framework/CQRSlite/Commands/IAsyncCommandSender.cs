using System.Threading.Tasks;

namespace CQRSlite.Commands
{
    public interface IAsyncCommandSender
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}