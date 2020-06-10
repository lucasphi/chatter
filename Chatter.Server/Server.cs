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

        public Server(
            IMediator mediator,
            IPacketFactory packetFactory)
        {
            _mediator = mediator;
            _packetFactory = packetFactory;
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
                var packet = _packetFactory.CreatePacket(client.GetStream()) as RegisterRequest;
                packet.Stream = client.GetStream();
                var createUserResult = _mediator.Send(packet).Result;
                if (createUserResult.Success)
                {
                    HandleClientConnection(client);
                }
            }
            catch (Exception)
            { }
        }

        private void HandleClientConnection(TcpClient client)
        {
            try
            {
                var stream = client.GetStream();
                var bytes = new byte[4096];
                while (true)
                {
                    stream.Read(bytes, 0, bytes.Length);
                    //var text = Encoding.UTF8.GetString(bytes);
                    //_output.Write(text);
                }
            }
            catch (Exception)
            {
                //_clients.Remove();
            }            
        }
    }
}
