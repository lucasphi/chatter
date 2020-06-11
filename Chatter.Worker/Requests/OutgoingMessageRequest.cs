using Chatter.Worker.Network;
using MediatR;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Worker.Requests
{
    public class OutgoingMessageRequest : Packet, IRequest<RequestResult>
    {
        public override byte PacketId => 3;

        public string Nickname { get; private set; }

        public string Message { get; private set; }

        public OutgoingMessageRequest(string nickname, string message)
        {
            Message = message;
            Nickname = nickname;
        }

        public OutgoingMessageRequest(IPacketReader packetReader, NetworkStream stream)
        {
            Nickname = ReadStringFromStream(packetReader, stream);
            Message = ReadStringFromStream(packetReader, stream);
        }
    }
}
