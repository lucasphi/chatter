using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Chatter.Worker.Exceptions
{
    [Serializable]
    public class InvalidConnectionException : Exception
    {
        public InvalidConnectionException(string message)
            : base(message)
        { }

        protected InvalidConnectionException(SerializationInfo info, StreamingContext context)
            : base (info, context)
        { }
    }
}
