using Chatter.Worker;
using Chatter.Worker.Network;
using Chatter.Worker.Requests;
using MediatR;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chatter.Server
{
    class Server
    {
        private readonly IMediator _mediator;
        private readonly IPacketFactory _packetFactory;
        private readonly IClientList _clientList;

        public Server(
            IMediator mediator,
            IPacketFactory packetFactory,
            IClientList clientList)
        {
            _mediator = mediator;
            _packetFactory = packetFactory;
            _clientList = clientList;
        }

        public void Run(string ipAddress, int port)
        {
            _mediator.Send(new PrintMessageRequest("Initializing server"));

            TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
            tcpListener.Start();

            _mediator.Send(new PrintMessageRequest("Server running. Awaiting connections"));

            StartListeningForConnections(tcpListener);
        }

        private void StartListeningForConnections(TcpListener tcpListener)
        {
            while(true)
            {
                var client = tcpListener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(AwaitClientHandshake, client, true);                
            }
        }

        private void AwaitClientHandshake(TcpClient client)
        {
            try
            {
                RequestResult createUserResult;
                do
                {
                    var packet = _packetFactory.CreatePacket(client.GetStream()) as RegisterRequest;
                    _clientList.AddConnectingClient(packet.UniqueId, client.GetStream());
                    createUserResult = _mediator.Send(packet).Result;
                } while (!createUserResult.Success);                

                HandleClientConnection(client);
            }
            catch (Exception)
            { }
        }

        private void HandleClientConnection(TcpClient client)
        {
            try
            {
                var stream = client.GetStream();
                while (true)
                {
                    var packet = _packetFactory.CreatePacket(client.GetStream());
                    _mediator.Send(packet);
                }
            }
            catch (Exception)
            {
                //_clients.Remove();
            }            
        }
    }
}
