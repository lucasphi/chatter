using Chatter.Worker.Network;
using MediatR;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Worker.Requests
{
    public class RegisterResultRequest : Packet, IRequest<SocketResult>
    {
        public override byte PacketId => 2;

        public string Nickname { get; private set; }

        public bool Registered { get; private set; }

        public RegisterResultRequest(bool registered, string nickname)
        {
            Registered = registered;
            Nickname = nickname;
        }

        public RegisterResultRequest(IPacketReader packetReader, NetworkStream stream)
        {
            var bytes = new byte[4];
            stream.Read(bytes, 0, 4);
            var lenght = packetReader.ConvertByteArrayToInt(bytes);
            var data = new byte[lenght];
            stream.Read(data, 0, data.Length);
            Nickname = Encoding.UTF8.GetString(data);
            var registered = new byte[1];
            stream.Read(registered, 0, 1);
            Registered = packetReader.ConvertByteToBoolean(registered[0]);
        }
    }
}
