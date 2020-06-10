using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Chatter.Server
{
    public interface IClientList
    {
        Dictionary<string, NetworkStream> Clients { get; }

        void PromoteClient(string nickname, Guid uniqueId);

        NetworkStream GetConnectingClient(Guid uniqueId);

        void AddConnectingClient(Guid uniqueId, NetworkStream stream);
    }
}
