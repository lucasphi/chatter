using Chatter.Worker.Exceptions;
using Chatter.Worker.Requests;
using MediatR;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Chatter.Client
{
    class Client : IClient
    {
        private readonly IMediator _mediator;
        private TcpClient _client;

        public NetworkStream Stream { get; private set; }

        public Client(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Connect(string server, int port)
        {
            if (_client != null && _client.Connected)
            {
                throw new InvalidConnectionException("There is an active connection already");
            }
            
            _client = new TcpClient(server, port);
            Stream = _client.GetStream();
            await RegisterClient();
        }

        private async Task RegisterClient()
        {
            await _mediator.Send(new PrintMessageRequest("*** Welcome to our chat server. Please provide a nickname"));            
            bool registered;
            string nickname;
            do
            {
                nickname = Console.ReadLine();
                registered = (await _mediator.Send(new RegisterRequest(nickname))).Success;
                if (!registered)
                {
                    await _mediator.Send(new PrintMessageRequest($"*** Sorry, the nickname {nickname} is already taken. Please choose a different one:"));
                }
            } while (!registered);
            await _mediator.Send(new PrintMessageRequest($"You are registered as {nickname}"));
        }
    }
}
