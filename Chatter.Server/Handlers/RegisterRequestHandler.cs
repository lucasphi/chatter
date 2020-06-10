using Chatter.Worker.Network;
using Chatter.Worker.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Server.Handlers
{
    class RegisterRequestHandler : IRequestHandler<RegisterRequest, SocketResult>
    {
        private readonly IClientList _clientList;
        private readonly IMediator _mediator;

        public RegisterRequestHandler(
            IClientList clientList,
            IMediator mediator)
        {
            _clientList = clientList;
            _mediator = mediator;
        }

        public async Task<SocketResult> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            bool nicknameIsFree = !_clientList.Clients.ContainsKey(request.Nickname);            
            if (nicknameIsFree)
            {
                AddClientToClientList(request);
            }
            await _mediator.Send(new RegisterResultRequest(nicknameIsFree, request.Nickname));
            return new SocketResult() { Success = nicknameIsFree };
        }

        private void AddClientToClientList(RegisterRequest request)
        {
            _clientList.Clients.Add(request.Nickname, request.Stream);
        }
    }
}
