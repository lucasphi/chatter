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
            var lengthBytes = new byte[4];
            stream.Read(lengthBytes, 0, 4);
            var lenght = packetReader.ConvertByteArrayToInt(lengthBytes);

            var data = new byte[lenght];
            stream.Read(data, 0, data.Length);
            Nickname = Encoding.UTF8.GetString(data);

            var registered = new byte[1];
            stream.Read(registered, 0, 1);
            Registered = packetReader.ConvertByteToBoolean(registered[0]);
        }
    }
}
