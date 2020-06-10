using Chatter.Worker;
using Chatter.Worker.Network;
using Chatter.Worker.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Client.Handlers
{
    class RegisterRequestHandler : IRequestHandler<RegisterRequest, SocketResult>
    {
        private readonly IStream _stream;
        private readonly IPacketWriter _packetWriter;

        public RegisterRequestHandler(
            IStream stream,
            IPacketWriter packetWriter)
        {
            _stream = stream;
            _packetWriter = packetWriter;
        }

        public Task<SocketResult> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            byte[] packet = ConvertRequestToByteArray(request);
            _stream.Stream.Write(packet, 0, packet.Length);
            return Task.FromResult(new SocketResult() { Success = true });
        }

        private byte[] ConvertRequestToByteArray(RegisterRequest request)
        {            
            _packetWriter.WriteByte(request.PacketId);
            _packetWriter.WriteInt(request.Nickname.Length);
            _packetWriter.WriteString(request.Nickname);

            return _packetWriter.GetBytes();
        }
    }
}
