using Chatter.Worker.Network;
using MediatR;
using System;
using System.IO;

namespace Chatter.Worker.Requests
{
    public class RegisterRequest : Packet, IRequest<RequestResult>
    {
        public override byte PacketId => 1;

        public Guid UniqueId { get; private set; } = Guid.NewGuid();

        public string Nickname { get; private set; }

        public RegisterRequest()
        { }

        public RegisterRequest(IPacketReader packetReader, Stream stream)
        {
            UniqueId = ReadGuidFromStream(packetReader, stream);
            Nickname = ReadStringFromStream(packetReader, stream);
        }
    }
}
