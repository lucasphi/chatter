using Chatter.Worker.Network;
using MediatR;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Worker.Requests
{
    public class RegisterRequest : Packet, IRequest<SocketResult>
    {
        public override byte PacketId => 1;

        public string Message { get; set; }

        public RegisterRequest(string username)
        {
            Message = username;
        }

        public RegisterRequest(IPacketReader packetReader, NetworkStream stream)
        {
            var bytes = new byte[4];
            stream.Read(bytes, 0, 4);
            var lenght = packetReader.ConvertByteArrayToInt(bytes);
            var data = new byte[lenght];
            stream.Read(data, 0, data.Length);
            Message = Encoding.UTF8.GetString(data);
        }
    }
}
