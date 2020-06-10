using Chatter.Worker.Network;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Worker.Requests
{
    class RegisterRequestHandler : IRequestHandler<RegisterRequest, SocketResult>
    {
        private readonly IStream _stream;
        private readonly IPacketWriter _packetWriter;
        private readonly IPacketReader _packetReader;

        public RegisterRequestHandler(
            IStream stream,
            IPacketWriter packetWriter,
            IPacketReader packetReader)
        {
            _stream = stream;
            _packetWriter = packetWriter;
            _packetReader = packetReader;
        }

        public Task<SocketResult> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            SendHandshakePacket(request);
            bool serverResponse = RetrieveServerResponse();
            return Task.FromResult(new SocketResult() { Success = Convert.ToBoolean(serverResponse) });
        }

        private void SendHandshakePacket(RegisterRequest request)
        {
            byte[] packet = ConvertRequestToByteArray(request);
            _stream.Stream.Write(packet, 0, packet.Length);
        }

        private byte[] ConvertRequestToByteArray(RegisterRequest request)
        {            
            _packetWriter.WriteByte(request.PacketId);
            _packetWriter.WriteInt(request.Message.Length);
            _packetWriter.WriteString(request.Message);

            return _packetWriter.GetBytes();
        }

        private bool RetrieveServerResponse()
        {
            var data = new byte[2];
            _stream.Stream.Read(data, 0, 2);
            return _packetReader.ConvertByteToBoolean(data[1]);
        }
    }
}
