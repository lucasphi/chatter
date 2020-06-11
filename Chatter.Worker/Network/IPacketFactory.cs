using MediatR;
using System.IO;

namespace Chatter.Worker.Network
{
    public interface IPacketFactory
    {
        IRequest<RequestResult> CreatePacket(Stream stream);
    }
}
