using System;
using System.Runtime.Serialization;

namespace Chatter.Worker.Exceptions
{
    [Serializable]
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException()
            : base()
        { }

        protected InvalidCommandException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
