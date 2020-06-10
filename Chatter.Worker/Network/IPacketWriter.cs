namespace Chatter.Worker.Network
{
    public interface IPacketWriter
    {
        byte[] GetBytes();

        void WriteByte(byte value);

        void WriteBool(bool value);

        void WriteInt(int value);

        void WriteString(string value);
    }
}
