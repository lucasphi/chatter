using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.Worker.Requests
{
    class PublicMessageRequestHandler
    {
        private readonly IStream _stream;

        public PublicMessageRequestHandler(IStream stream)
        {
            _stream = stream;
        }
    }
}
