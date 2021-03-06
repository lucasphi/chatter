﻿using Chatter.Worker.Exceptions;
using Chatter.Worker.Requests;
using MediatR;
using System.IO;

namespace Chatter.Worker.Network
{
    public class PacketFactory : IPacketFactory
    {
        private readonly IPacketReader _packetReader;

        public PacketFactory(IPacketReader packetReader)
        {
            _packetReader = packetReader;
        }

        public IRequest<RequestResult> CreatePacket(Stream stream)
        {
            var bytes = new byte[1];
            stream.Read(bytes, 0, bytes.Length);
            var packetId = bytes[0];
            return packetId switch
            {
                1 => new RegisterRequest(_packetReader, stream),
                2 => new RegisterResultRequest(_packetReader, stream),
                3 => new OutgoingMessageRequest(_packetReader, stream),
                4 => new IncomingMessageRequest(_packetReader, stream),
                _ => throw new InvalidPacketException(),
            };
        }
    }
}
