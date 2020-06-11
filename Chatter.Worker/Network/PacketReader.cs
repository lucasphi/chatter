using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.Worker.Network
{
    public class PacketReader : IPacketReader
    {
        public int ConvertByteArrayToInt(byte[] bytes)
        {
            if (bytes.Length != 4)
            {
                throw new ArgumentException("Byte array does not have Int length");
            }

            return (bytes[0] << 24) | 
                   (bytes[1] << 16) | 
                   (bytes[2] << 8) | 
                   bytes[3];
        }

        public bool ConvertByteToBoolean(byte value)
        {
            return Convert.ToBoolean(value);
        }

        public Guid ConvertByteArrayToGuid(byte[] bytes)
        {
            if (bytes.Length != 16)
            {
                throw new ArgumentException("Byte array does not have Guid length");
            }

            return new Guid(bytes);
        }
    }
}
