using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.Worker.Network
{
    public interface IPacketReader
    {
        int ConvertByteArrayToInt(byte[] bytes);

        bool ConvertByteToBoolean(byte value);
    }
}
