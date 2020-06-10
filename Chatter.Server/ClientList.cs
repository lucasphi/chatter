using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Chatter.Server
{
    public class ClientList : IClientList
    {
        public ConcurrentDictionary<string, NetworkStream> Clients { get; } = new ConcurrentDictionary<string, NetworkStream>();

        private Dictionary<Guid, NetworkStream> JoinClients { get; } = new Dictionary<Guid, NetworkStream>();

        public void PromoteClient(string nickname, Guid uniqueId)
        {
            Clients.TryAdd(nickname, JoinClients[uniqueId]);
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
