using Chatter.Worker.Network;
using MediatR;

namespace Chatter.Worker.Requests
{
    public class RegisterResultRequest : Packet, IRequest<SocketResult>
    {
        public override byte PacketId => 2;

        public bool Success { get; private set; }

        public RegisterResultRequest(bool success)
        {
            Success = success;
        }
    }
}
