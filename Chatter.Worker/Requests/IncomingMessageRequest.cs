using Chatter.Worker.Network;
using MediatR;
using System.IO;
using System.Net.Sockets;

namespace Chatter.Worker.Requests
{
    public class IncomingMessageRequest : Packet, IRequest<RequestResult>
    {
        public override byte PacketId => 4;

        public string Nickname { get; private set; }        

        public string Message { get; private set; }

        public string Destination { get; private set; }

        public bool PrivateMessage { get; private set; }

        public IncomingMessageRequest(
            string nickname,
            string message,
            string destination,
            bool privateMessage)
        {
            Message = message;
            Nickname = nickname;
            Destination = destination;
            PrivateMessage = privateMessage;
        }

        public IncomingMessageRequest(IPacketReader packetReader, Stream stream)
        {
            Nickname = ReadStringFromStream(packetReader, stream);
            Message = ReadStringFromStream(packetReader, stream);
            var hasDestination = ReadBooleanFromStream(packetReader, stream);
            if (hasDestination)
            {
                Destination = ReadStringFromStream(packetReader, stream);
                PrivateMessage = ReadBooleanFromStream(packetReader, stream);
            }
        }
    }
}
