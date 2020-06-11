using Chatter.Worker;
using Chatter.Worker.Network;
using Chatter.Worker.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Server.Handlers
{
    public class RegisterResultRequestHandler : IRequestHandler<RegisterResultRequest, RequestResult>
    {
        private readonly IClientList _clientList;
        private readonly IPacketWriter _packetWriter;

        public RegisterResultRequestHandler(
            IClientList clientList,
            IPacketWriter packetWriter)
        {
            _clientList = clientList;
            _packetWriter = packetWriter;
        }

        public Task<RequestResult> Handle(RegisterResultRequest request, CancellationToken cancellationToken)
        {
            var stream = _clientList.GetConnectingClient(request.UniqueId);
            byte[] packet = ConvertRequestToByteArray(request);
            stream.Write(packet, 0, packet.Length);
            return Task.FromResult(new RequestResult() { Success = true });
        }

        private byte[] ConvertRequestToByteArray(RegisterResultRequest request)
        {
            _packetWriter.WriteByte(request.PacketId);
            _packetWriter.WriteString(request.Nickname);
            _packetWriter.WriteBool(request.Registered);
            return _packetWriter.GetBytes();
        }
    }
}
