using Chatter.Worker;
using System.Threading.Tasks;

namespace Chatter.Client
{
    public interface IClient : IStream
    {
        Task Connect(string server, int port);

        void AwaitCommand();
    }
}
