using Chatter.Worker.Network;
using MediatR;
using System;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Worker.Requests
{
    public class RegisterRequest : Packet, IRequest<RequestResult>
    {
        public override byte PacketId => 1;

        public Guid UniqueId { get; private set; } = Guid.NewGuid();

        public string Nickname { get; private set; }

        public RegisterRequest()
        { }

        public RegisterRequest(IPacketReader packetReader, NetworkStream stream)
        {
            UniqueId = ReadGuidFromStream(packetReader, stream);
            Nickname = ReadStringFromStream(packetReader, stream);
        }
    }
}
