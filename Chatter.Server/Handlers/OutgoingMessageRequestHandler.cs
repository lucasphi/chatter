using Chatter.Worker;
using Chatter.Worker.Network;
using Chatter.Worker.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Server.Handlers
{
    public class OutgoingMessageRequestHandler : IRequestHandler<OutgoingMessageRequest, RequestResult>
    {
        private readonly IClientList _clientList;
        private readonly IPacketWriter _packetWriter;

        public OutgoingMessageRequestHandler(
            IClientList clientList,
            IPacketWriter packetWriter)
        {
            _clientList = clientList;
            _packetWriter = packetWriter;
        }

        public Task<RequestResult> Handle(OutgoingMessageRequest request, CancellationToken cancellationToken)
        {
            //TODO: split the commands into Requests
            if (request.Message.StartsWith("/m"))
            {

            }
            else if (request.Message.StartsWith("/p"))
            {

            }
            else
            {
                SendMessageToAllClients(request);
            }
            return Task.FromResult(new RequestResult() { Success = true });
        }

        private void SendMessageToAllClients(OutgoingMessageRequest request)
        {
            byte[] packet = ConvertRequestToByteArray(request);
            foreach (var client in _clientList.Clients.Values)
            {
                client.Write(packet, 0, packet.Length);
            }
        }

        private byte[] ConvertRequestToByteArray(OutgoingMessageRequest request)
        {
            var reponseRequest = new IncomingMessageRequest(request.Nickname, request.Message);
            _packetWriter.WriteByte(reponseRequest.PacketId);
            _packetWriter.WriteString(reponseRequest.Nickname);
            _packetWriter.WriteString(reponseRequest.Message);
            return _packetWriter.GetBytes();
        }
    }
}
