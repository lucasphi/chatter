using Chatter.Worker.Network;
using MediatR;
using System.Net.Sockets;

namespace Chatter.Worker.Requests
{
    public class IncomingMessageRequest : Packet, IRequest<RequestResult>
    {
        public override byte PacketId => 4;

        public string Nickname { get; private set; }

        public string Message { get; private set; }

        public IncomingMessageRequest(string nickname, string message)
        {
            Message = message;
            Nickname = nickname;
        }

        public IncomingMessageRequest(IPacketReader packetReader, NetworkStream stream)
        {
            Nickname = ReadStringFromStream(packetReader, stream);
            Message = ReadStringFromStream(packetReader, stream);
        }
    }
}
