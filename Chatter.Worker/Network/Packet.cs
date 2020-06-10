using System.Net.Sockets;

namespace Chatter.Worker.Network
{
    public abstract class Packet
    {
        public abstract byte PacketId { get; }

        public NetworkStream Stream { get; set; }
    }
}
