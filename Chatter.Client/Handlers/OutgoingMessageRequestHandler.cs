using Chatter.Worker;
using Chatter.Worker.Exceptions;
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
        private readonly IMediator _mediator;

        public OutgoingMessageRequestHandler(
            IStream stream,
            IPacketWriter packetWriter,
            IMediator mediator)
        {
            _stream = stream;
            _packetWriter = packetWriter;
            _mediator = mediator;
        }

        public Task<RequestResult> Handle(OutgoingMessageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                byte[] packet = ConvertRequestToByteArray(request);
                _stream.Stream.Write(packet, 0, packet.Length);
                return Task.FromResult(new RequestResult() { Success = true });
            }
            catch (InvalidCommandException)
            {
                _mediator.Send(new PrintMessageRequest("Invalid command format. Please type /help to see available commands"));
            }
            return Task.FromResult(new RequestResult() { Success = true });
        }

        private byte[] ConvertRequestToByteArray(OutgoingMessageRequest request)
        {
            _packetWriter.WriteByte(request.PacketId);
            _packetWriter.WriteString(request.Nickname);

            if (request.Message.StartsWith("/p") || request.Message.StartsWith("/m"))
            {
                WriteTargetedMessages(request);
            }
            else
            {
                WriteGlobalMessage(request);
            }

            return _packetWriter.GetBytes();
        }

        private void WriteTargetedMessages(OutgoingMessageRequest request)
        {
            var desconstructedMessage = request.Message.Split(' ');
            if (desconstructedMessage.Length < 3)
            {
                throw new InvalidCommandException();
            }
            _packetWriter.WriteString(ReconstructMessage(desconstructedMessage));
            _packetWriter.WriteBool(true);
            _packetWriter.WriteString(desconstructedMessage[1]);
            _packetWriter.WriteBool(request.Message.StartsWith("/m"));
        }

        private static string ReconstructMessage(string[] desconstructedMessage)
        {
            return string.Join(' ', desconstructedMessage, 2, desconstructedMessage.Length - 2);
        }

        private void WriteGlobalMessage(OutgoingMessageRequest request)
        {
            _packetWriter.WriteString(request.Message);
            _packetWriter.WriteBool(false);
        }
    }
}
