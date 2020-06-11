using System;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Worker.Network
{
    public abstract class Packet
    {
        public abstract byte PacketId { get; }        

        protected string ReadStringFromStream(IPacketReader packetReader, NetworkStream stream)
        {
            var lengthBytes = new byte[4];
            stream.Read(lengthBytes, 0, 4);
            var lenght = packetReader.ConvertByteArrayToInt(lengthBytes);

            var data = new byte[lenght];
            stream.Read(data, 0, data.Length);
            return Encoding.UTF8.GetString(data);
        }

        protected Guid ReadGuidFromStream(IPacketReader packetReader, NetworkStream stream)
        {
            var idBytes = new byte[16];
            stream.Read(idBytes, 0, idBytes.Length);
            return packetReader.ConvertByteArrayToGuid(idBytes);
        }

        protected bool ReadBooleanFromStream(IPacketReader packetReader, NetworkStream stream)
        {
            var registered = new byte[1];
            stream.Read(registered, 0, 1);
            return packetReader.ConvertByteToBoolean(registered[0]);
        }
    }
}
