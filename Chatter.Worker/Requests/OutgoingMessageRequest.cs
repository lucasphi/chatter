using Chatter.Worker.Network;
using MediatR;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Worker.Requests
{
    public class OutgoingMessageRequest : Packet, IRequest<RequestResult>
    {
        public override byte PacketId => 3;

        public string Nickname { get; private set; }        

        public string Message { get; private set; }

        public string Destination { get; private set; }

        public bool PrivateMessage { get; private set; }

        public OutgoingMessageRequest(string nickname, string message)
        {
            Message = message;
            Nickname = nickname;
        }

        public OutgoingMessageRequest(IPacketReader packetReader, Stream stream)
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
