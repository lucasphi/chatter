using Chatter.Worker;
using Chatter.Worker.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Client.Handlers
{
    public class IncomingMessageRequestHandler : IRequestHandler<IncomingMessageRequest, RequestResult>
    {
        private readonly IMediator _mediator;

        public IncomingMessageRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<RequestResult> Handle(IncomingMessageRequest request, CancellationToken cancellationToken)
        {
            _mediator.Send(new PrintMessageRequest($"\"{request.Nickname}\" says: {request.Message}"));
            return Task.FromResult(new RequestResult() { Success = true });
        }
    }
}
