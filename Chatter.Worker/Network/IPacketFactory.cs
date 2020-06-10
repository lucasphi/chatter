using MediatR;
using System.Net.Sockets;

namespace Chatter.Worker.Network
{
    public interface IPacketFactory
    {
        IRequest<SocketResult> CreatePacket(NetworkStream stream);
    }
}
