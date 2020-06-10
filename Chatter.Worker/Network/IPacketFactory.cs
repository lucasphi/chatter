using MediatR;
using System.Net.Sockets;

namespace Chatter.Worker.Network
{
    public interface IPacketFactory
    {
        IRequest<RequestResult> CreatePacket(NetworkStream stream);
    }
}
