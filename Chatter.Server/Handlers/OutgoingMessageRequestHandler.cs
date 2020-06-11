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
        private readonly IMediator _mediator;
        private readonly IClientList _clientList;
        private readonly IPacketWriter _packetWriter;

        public OutgoingMessageRequestHandler(
            IMediator mediator,
            IClientList clientList,
            IPacketWriter packetWriter)
        {
            _mediator = mediator;
            _clientList = clientList;
            _packetWriter = packetWriter;
        }

        public Task<RequestResult> Handle(OutgoingMessageRequest request, CancellationToken cancellationToken)
        {            
            if (request.Message.StartsWith("/m"))
            {

            }
            else if (request.Message.StartsWith("/p"))
            {

            }
            else
            {
                foreach (var client in _clientList.Clients.Values)
                {

                }
            }
            throw new NotImplementedException();
        }

        private byte[] ConvertRequestToByteArray(OutgoingMessageRequest request)
        {
            _packetWriter.WriteByte(request.PacketId);
            _packetWriter.WriteString(request.Message);

            return _packetWriter.GetBytes();
        }
    }
}
