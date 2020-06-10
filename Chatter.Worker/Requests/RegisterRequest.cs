using Chatter.Worker.Network;
using MediatR;
using System;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Worker.Requests
{
    public class RegisterRequest : Packet, IRequest<SocketResult>
    {
        public override byte PacketId => 1;

        public Guid UniqueId { get; private set; } = Guid.NewGuid();

        public string Nickname { get; private set; }

        public RegisterRequest()
        { }

        public RegisterRequest(IPacketReader packetReader, NetworkStream stream)
        {
            var idBytes = new byte[16];
            stream.Read(idBytes, 0, idBytes.Length);
            UniqueId = packetReader.ConvertByteArrayToGuid(idBytes);

            var lengthBytes = new byte[4];
            stream.Read(lengthBytes, 0, 4);
            var lenght = packetReader.ConvertByteArrayToInt(lengthBytes);

            var data = new byte[lenght];
            stream.Read(data, 0, data.Length);
            Nickname = Encoding.UTF8.GetString(data);
        }
    }
}
