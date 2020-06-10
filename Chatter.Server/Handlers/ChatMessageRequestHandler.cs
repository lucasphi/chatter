using Chatter.Worker;
using Chatter.Worker.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Server.Handlers
{
    public class ChatMessageRequestHandler : IRequestHandler<ChatMessageRequest, RequestResult>
    {
        private readonly IMediator _mediator;

        public ChatMessageRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<RequestResult> Handle(ChatMessageRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
