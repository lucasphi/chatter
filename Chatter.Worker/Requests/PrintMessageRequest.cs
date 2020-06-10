using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatter.Worker.Requests
{
    public class PrintMessageRequest : IRequest
    {
        public string Message { get; }

        public PrintMessageRequest(string message)
        {
            Message = message;
        }
    }
}
