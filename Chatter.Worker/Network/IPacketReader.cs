using System;

namespace Chatter.Worker.Network
{
    public interface IPacketReader
    {
        int ConvertByteArrayToInt(byte[] bytes);

        bool ConvertByteToBoolean(byte value);

        Guid ConvertByteArrayToGuid(byte[] bytes);
    }
}
