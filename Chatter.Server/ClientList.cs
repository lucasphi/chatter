using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Chatter.Server
{
    public class ClientList : IClientList
    {
        public Dictionary<string, NetworkStream> Clients { get; } = new Dictionary<string, NetworkStream>();

        private Dictionary<Guid, NetworkStream> JoinClients { get; } = new Dictionary<Guid, NetworkStream>();

        public void PromoteClient(string nickname, Guid uniqueId)
        {
            Clients.Add(nickname, JoinClients[uniqueId]);
            JoinClients.Remove(uniqueId);
        }

        public NetworkStream GetConnectingClient(Guid uniqueId)
        {
            return JoinClients.ContainsKey(uniqueId) ? JoinClients[uniqueId] : null;
        }

        public void AddConnectingClient(Guid uniqueId, NetworkStream stream)
        {
            JoinClients.Add(uniqueId, stream);
        }
    }
}
