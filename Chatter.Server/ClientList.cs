using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Server
{
    public class ClientList : IClientList
    {
        public Dictionary<string, NetworkStream> Clients { get; } = new Dictionary<string, NetworkStream>();
    }
}
