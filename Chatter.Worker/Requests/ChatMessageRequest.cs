using Chatter.Worker.Network;
using MediatR;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Worker.Requests
{
    public class ChatMessageRequest : Packet, IRequest<RequestResult>
    {
        public override byte PacketId => 3;

        public string Message { get; private set; }

        public ChatMessageRequest(string message)
        {
            Message = message;
        }

        public ChatMessageRequest(IPacketReader packetReader, NetworkStream stream)
        {
            var lengthBytes = new byte[4];
            stream.Read(lengthBytes, 0, 4);
            var lenght = packetReader.ConvertByteArrayToInt(lengthBytes);

            var data = new byte[lenght];
            stream.Read(data, 0, data.Length);
            Message = Encoding.UTF8.GetString(data);
        }
    }
}
