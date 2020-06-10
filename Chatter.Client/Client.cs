using Chatter.Worker.Exceptions;
using Chatter.Worker.Network;
using Chatter.Worker.Requests;
using MediatR;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.Client
{
    class Client : IClient
    {
        private readonly IMediator _mediator;
        private readonly IPacketFactory _packetFactory;
        private TcpClient _client;

        public NetworkStream Stream { get; private set; }

        public Client(
            IMediator mediator,
            IPacketFactory packetFactory)
        {
            _mediator = mediator;
            _packetFactory = packetFactory;
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

            HandleConnection();
        }

        private async Task RegisterClient()
        {
            await _mediator.Send(new PrintMessageRequest("*** Welcome to our chat server. Please provide a nickname"));
            await _mediator.Send(new RegisterRequest());
        }

        private void HandleConnection()
        {
            while(true)
            {
                var packet = _packetFactory.CreatePacket(_client.GetStream());
                _mediator.Send(packet);
            }
        }

        public void AwaitCommand()
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                while (true)
                {
                    var command = Console.ReadLine();
                    if (command.StartsWith("/help"))
                    {
                        _mediator.Send(new PrintMessageRequest(
                            "Available commands: \n/help (Display help)\n/m nickname message (Sends a public message to someone)\n/p nickname message (Sends a private message to someone)"));
                    }
                    else
                    {
                        _mediator.Send(new ChatMessageRequest(command));
                    }
                }
            });
        }
    }
}
