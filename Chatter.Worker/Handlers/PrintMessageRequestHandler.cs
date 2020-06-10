using Chatter.Worker.Requests;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Worker.Handlers
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
