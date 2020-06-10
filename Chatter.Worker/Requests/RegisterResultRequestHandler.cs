using Chatter.Worker.Network;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Worker.Requests
{
    public class RegisterResultRequestHandler : IRequestHandler<RegisterResultRequest, SocketResult>
    {
        private readonly IStream _stream;
        private readonly IPacketWriter _packetWriter;

        public RegisterResultRequestHandler(
            IStream stream,
            IPacketWriter packetWriter)
        {
            _stream = stream;
            _packetWriter = packetWriter;
        }

        public Task<SocketResult> Handle(RegisterResultRequest request, CancellationToken cancellationToken)
        {
            byte[] packet = ConvertRequestToByteArray(request);
            _stream.Stream.Write(packet, 0, packet.Length);
            return Task.FromResult(new SocketResult() { Success = true });
        }

        private byte[] ConvertRequestToByteArray(RegisterResultRequest request)
        {
            _packetWriter.WriteByte(request.PacketId);
            _packetWriter.WriteBool(request.Success);
            return _packetWriter.GetBytes();
        }
    }
}
