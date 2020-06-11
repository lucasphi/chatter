using Chatter.Worker.Network;
using MediatR;
using System;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Worker.Requests
{
    public class RegisterResultRequest : Packet, IRequest<RequestResult>
    {
        public override byte PacketId => 2;

        public Guid UniqueId { get; private set; }

        public string Nickname { get; private set; }

        public bool Registered { get; private set; }

        public RegisterResultRequest(
            bool registered,
            string nickname,
            Guid uniqueId)
        {
            Registered = registered;
            Nickname = nickname;
            UniqueId = uniqueId;
        }

        public RegisterResultRequest(IPacketReader packetReader, NetworkStream stream)
        {
            Nickname = ReadStringFromStream(packetReader, stream);
            Registered = ReadBooleanFromStream(packetReader, stream);
        }
    }
}
