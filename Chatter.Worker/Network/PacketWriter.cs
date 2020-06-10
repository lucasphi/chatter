using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chatter.Worker.Network
{
    public class PacketWriter : IPacketWriter
    {
        private readonly MemoryStream _memoryStream = new MemoryStream();

        public byte[] GetBytes()
        {   
            _memoryStream.Seek(0, SeekOrigin.Begin);
            return _memoryStream.ToArray();
        }

        public void WriteByte(byte value)
        {
            _memoryStream.WriteByte(value);
        }

        public void WriteBool(bool value)
        {
            _memoryStream.WriteByte(Convert.ToByte(value));
        }

        public void WriteInt(int value)
        {
            byte[] buffer = new byte[4];
            buffer[0] = (byte)(value >> 24);
            buffer[1] = (byte)(value >> 16);
            buffer[2] = (byte)(value >> 8);
            buffer[3] = (byte)value;
            _memoryStream.Write(buffer, 0, 4);
        }

        public void WriteString(string value)
        {
            _memoryStream.Write(Encoding.UTF8.GetBytes(value), 0, value.Length);
        }

        public void WriteGuid(Guid value)
        {
            byte[] guidBytes = value.ToByteArray();
            _memoryStream.Write(guidBytes, 0, guidBytes.Length);
        }
    }
}
