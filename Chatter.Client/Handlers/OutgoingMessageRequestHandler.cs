using Chatter.Worker;
using Chatter.Worker.Network;
using Chatter.Worker.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Client.Handlers
{
    public class OutgoingMessageRequestHandler : IRequestHandler<OutgoingMessageRequest, RequestResult>
    {
        private readonly IStream _stream;
        private readonly IPacketWriter _packetWriter;

        public OutgoingMessageRequestHandler(
            IStream stream,
            IPacketWriter packetWriter)
        {
            _stream = stream;
            _packetWriter = packetWriter;
        }

        public Task<RequestResult> Handle(OutgoingMessageRequest request, CancellationToken cancellationToken)
        {
            byte[] packet = ConvertRequestToByteArray(request);
            _stream.Stream.Write(packet, 0, packet.Length);
            return Task.FromResult(new RequestResult() { Success = true });
        }

        private byte[] ConvertRequestToByteArray(OutgoingMessageRequest request)
        {
            _packetWriter.WriteByte(request.PacketId);
            _packetWriter.WriteString(request.Nickname);
            _packetWriter.WriteString(request.Message);

            return _packetWriter.GetBytes();
        }
    }
}
