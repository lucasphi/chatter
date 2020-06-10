using Chatter.Worker.Network;
using MediatR;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Worker.Requests
{
    public class RegisterRequest : Packet, IRequest<SocketResult>
    {
        public override byte PacketId => 1;

        public string Nickname { get; set; }

        public RegisterRequest(string username)
        {
            Nickname = username;
        }

        public RegisterRequest(IPacketReader packetReader, NetworkStream stream)
        {
            var bytes = new byte[4];
            stream.Read(bytes, 0, 4);
            var lenght = packetReader.ConvertByteArrayToInt(bytes);
            var data = new byte[lenght];
            stream.Read(data, 0, data.Length);
            Nickname = Encoding.UTF8.GetString(data);
        }
    }
}
