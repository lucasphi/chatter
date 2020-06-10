using System;
using System.Runtime.Serialization;

namespace Chatter.Worker.Exceptions
{
    [Serializable]
    public class InvalidPacketException : Exception
    {
        public InvalidPacketException()
            : base()
        { }

        protected InvalidPacketException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
