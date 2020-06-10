using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Chatter.Server
{
    public interface IClientList
    {
        ConcurrentDictionary<string, NetworkStream> Clients { get; }

        void PromoteClient(string nickname, Guid uniqueId);

        NetworkStream GetConnectingClient(Guid uniqueId);

        void AddConnectingClient(Guid uniqueId, NetworkStream stream);
    }
}
