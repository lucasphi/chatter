using Chatter.Worker;
using Chatter.Worker.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Client.Handlers
{
    public class RegisterResultRequestHandler : IRequestHandler<RegisterResultRequest, RequestResult>
    {
        private readonly IMediator _mediator;
        private readonly IClient _client;

        public RegisterResultRequestHandler(
            IMediator mediator,
            IClient client)
        {
            _mediator = mediator;
            _client = client;
        }

        public Task<RequestResult> Handle(RegisterResultRequest request, CancellationToken cancellationToken)
        {            
            if (request.Registered)
            {
                _mediator.Send(new PrintMessageRequest($"You are registered as {request.Nickname}"));
                _mediator.Send(new PrintMessageRequest($"Type /help to see the list of all available commands"));
                _client.AwaitCommand();
            }
            else
            {
                _mediator.Send(new PrintMessageRequest($"*** Sorry, the nickname {request.Nickname} is already taken. Please choose a different one:"));
                _mediator.Send(new RegisterRequest());
            }
            return Task.FromResult(new RequestResult() { Success = request.Registered });
        }
    }
}
