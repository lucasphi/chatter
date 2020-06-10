using Chatter.Worker.Exceptions;
using Chatter.Worker.Requests;
using MediatR;
using System.Net.Sockets;

namespace Chatter.Worker.Network
{
    public class PacketFactory : IPacketFactory
    {
        private readonly IPacketReader _packetReader;

        public PacketFactory(IPacketReader packetReader)
        {
            _packetReader = packetReader;
        }

        public IRequest<SocketResult> CreatePacket(NetworkStream stream)
        {
            var bytes = new byte[1];
            stream.Read(bytes, 0, bytes.Length);
            var packetId = bytes[0];
            return packetId switch
            {
                1 => new RegisterRequest(_packetReader, stream),
                2 => new RegisterResultRequest(_packetReader, stream),
                _ => throw new InvalidPacketException(),
            };
        }
    }
}
