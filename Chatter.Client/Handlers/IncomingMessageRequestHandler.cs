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
            if (string.IsNullOrWhiteSpace(request.Destination))
            {
                _mediator.Send(new PrintMessageRequest($"{request.Nickname} says: {request.Message}"));
            }
            else if (!request.PrivateMessage)
            {
                _mediator.Send(new PrintMessageRequest($"{request.Nickname} says to {request.Destination}: {request.Message}"));
            }
            else
            {
                _mediator.Send(new PrintMessageRequest($"{request.Nickname} says privetely to {request.Destination}: {request.Message}"));
            }
            return Task.FromResult(new RequestResult() { Success = true });
        }
    }
}
