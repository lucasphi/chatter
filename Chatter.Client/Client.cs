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
            string nickname = Console.ReadLine();
            await _mediator.Send(new RegisterRequest(nickname));
        }

        private void HandleConnection()
        {
            while(true)
            {
                var packet = _packetFactory.CreatePacket(_client.GetStream());
                _mediator.Send(packet);
            }
        }
    }
}
