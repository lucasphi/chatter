using MediatR;

namespace Chatter.Worker.Network
{
    public abstract class Packet
    {
        public abstract byte PacketId { get; }
    }
}
