using Chatter.Worker.Exceptions;
using Chatter.Worker.Network;
using Chatter.Worker.Requests;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Chatter.UnitTests.Worker.Network
{
    public class PacketFactoryTests
    {
        [Theory]
        [MemberData(nameof(Packets), MemberType = typeof(PacketFactoryTests))]
        public void CreatePacket(byte[] value, Type expectedType)
        {
            var streamMock = new MemoryStream(value);
            var factory = new PacketFactory(new PacketReader());

            var result = factory.CreatePacket(streamMock);

            result.GetType().Should().Be(expectedType);
        }

        [Fact]
        public void ShouldThrowOnInvalidPacketId()
        {
            var streamMock = new MemoryStream(new byte[1] { 10 });
            var factory = new PacketFactory(new PacketReader());

            Exception exception = Assert.Throws<InvalidPacketException>(() => factory.CreatePacket(streamMock));
            exception.Should().NotBeNull();
        }

        public static IEnumerable<object[]> Packets => new List<object[]>
        {
            new object[]
            {
                new byte[1] { 1 },
                typeof(RegisterRequest)
            },
            new object[]
            {
                new byte[1] { 2 },
                typeof(RegisterResultRequest)
            },
            new object[]
            {
                new byte[1] { 3 },
                typeof(OutgoingMessageRequest)
            },
            new object[]
            {
                new byte[1] { 4 },
                typeof(IncomingMessageRequest)
            },
        };
    }
}
