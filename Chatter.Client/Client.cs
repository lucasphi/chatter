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

        public void InitializeStreamListeningThread(string nickname)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                var running = true;
                while (running)
                {
                    var command = Console.ReadLine();
                    //TODO: split the commands into Requests
                    if (command.StartsWith("/help"))
                    {
                        DisplayHelpMessage();
                    }
                    else if(command.StartsWith("/exit"))
                    {
                        CloseConnection();
                        running = false;
                    }
                    else
                    {
                        _mediator.Send(new OutgoingMessageRequest(nickname, command));
                    }
                }
            });
        }

        private void DisplayHelpMessage()
        {
            _mediator.Send(new PrintMessageRequest(
                                        "*** Available commands: \n/help (Display help)\n/m nickname message (Sends a public message to someone)" +
                                        "\n/p nickname message (Sends a private message to someone)\n/exit (Disconnects)\n***"));
        }

        private void CloseConnection()
        {
            Stream.Close();            
            _mediator.Send(new PrintMessageRequest("*** Disconnected"));
        }
    }
}
