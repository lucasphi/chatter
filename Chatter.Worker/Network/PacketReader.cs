using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.Worker.Network
{
    public class PacketReader : IPacketReader
    {
        public int ConvertByteArrayToInt(byte[] bytes)
        {
            return (bytes[0] << 24) | 
                   (bytes[1] << 16) | 
                   (bytes[2] << 8) | 
                   bytes[3];
        }

        public bool ConvertByteToBoolean(byte value)
        {
            return Convert.ToBoolean(value);
        }
    }
}
