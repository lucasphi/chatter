using System.Collections.Generic;
using System.Net.Sockets;

namespace Chatter.Server
{
    public interface IClientList
    {
        Dictionary<string, NetworkStream> Clients { get; }
    }
}
