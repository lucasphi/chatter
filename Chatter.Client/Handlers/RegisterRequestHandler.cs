using Chatter.Worker;
using Chatter.Worker.Network;
using Chatter.Worker.Requests;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Client.Handlers
{
    class RegisterRequestHandler : IRequestHandler<RegisterRequest, RequestResult>
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

        public Task<RequestResult> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            string nickname = Console.ReadLine();
            byte[] packet = ConvertRequestToByteArray(request, nickname);
            _stream.Stream.Write(packet, 0, packet.Length);
            return Task.FromResult(new RequestResult() { Success = true });
        }

        private byte[] ConvertRequestToByteArray(RegisterRequest request, string nickname)
        {            
            _packetWriter.WriteByte(request.PacketId);
            _packetWriter.WriteGuid(request.UniqueId);
            _packetWriter.WriteString(nickname);

            return _packetWriter.GetBytes();
        }
    }
}
