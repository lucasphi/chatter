using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Chatter.Worker
{
    public interface IStream
    {
        NetworkStream Stream { get; }
    }
}
