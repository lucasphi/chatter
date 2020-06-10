using Chatter.Worker;
using System.Threading.Tasks;

namespace Chatter.Client
{
    interface IClient : IStream
    {
        Task Connect(string server, int port);
    }
}
