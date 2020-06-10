using Chatter.Worker;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Worker.Requests
{
    public class PrintMessageRequestHandler : IRequestHandler<PrintMessageRequest>
    {
        public Task<Unit> Handle(PrintMessageRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.Message);
            return Unit.Task;
        }
    }
}
