using System;
using System.IO;
using System.Text;

namespace Chatter.Worker.Network
{
    public abstract class Packet
    {
        public abstract byte PacketId { get; }        

        protected string ReadStringFromStream(IPacketReader packetReader, Stream stream)
        {
            var lengthBytes = new byte[4];
            stream.Read(lengthBytes, 0, 4);
            var lenght = packetReader.ConvertByteArrayToInt(lengthBytes);

            if (lenght == 0)
            {
                return string.Empty;
            }

            var data = new byte[lenght];
            stream.Read(data, 0, data.Length);
            return Encoding.UTF8.GetString(data);
        }

        protected Guid ReadGuidFromStream(IPacketReader packetReader, Stream stream)
        {
            var idBytes = new byte[16];
            stream.Read(idBytes, 0, idBytes.Length);
            return packetReader.ConvertByteArrayToGuid(idBytes);
        }

        protected bool ReadBooleanFromStream(IPacketReader packetReader, Stream stream)
        {
            var registered = new byte[1];
            stream.Read(registered, 0, 1);
            return packetReader.ConvertByteToBoolean(registered[0]);
        }
    }
}
