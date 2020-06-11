using Chatter.Worker.Network;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Chatter.UnitTests.Worker.Network
{
    public class PacketTests
    {
        [Fact]
        public void ReadStringFromStream()
        {
            var streamMock = new MemoryStream(new byte[28] { 0, 0, 0, 24, 84, 104, 105, 115, 32, 105, 115, 32, 97, 
                110, 32, 97, 119, 101, 115, 111, 109, 101, 32, 116, 101, 115, 116, 33 });
            var packet = new PacketMock();

            var result = packet.TestReadStringFromStream(new PacketReader(), streamMock);

            result.Should().Be("This is an awesome test!");
        }

        [Fact]
        public void ReadEmptyStringWontThrow()
        {
            var streamMock = new MemoryStream(new byte[4] { 0, 0, 0, 0 });
            var packet = new PacketMock();

            var result = packet.TestReadStringFromStream(new PacketReader(), streamMock);

            result.Should().Be(string.Empty);
        }

        [Fact]
        public void ReadGuidFromStream()
        {
            var streamMock = new MemoryStream(new byte[16] { 98, 111, 189, 219, 189, 110, 44, 70, 168, 8, 141, 230, 172, 68, 49, 58 });
            var packet = new PacketMock();

            var result = packet.TestReadGuidFromStream(new PacketReader(), streamMock);

            result.Should().Be(Guid.Parse("dbbd6f62-6ebd-462c-a808-8de6ac44313a"));
        }

        [Theory]
        [MemberData(nameof(BooleanBytes), MemberType = typeof(PacketTests))]
        public void ReadBooleanFromStream(byte[] value, bool expected)
        {
            var streamMock = new MemoryStream(value);
            var packet = new PacketMock();

            var result = packet.TestReadBooleanFromStream(new PacketReader(), streamMock);

            result.Should().Be(expected);
        }

        public static IEnumerable<object[]> BooleanBytes => new List<object[]>
        {
            new object[] { new byte[1] { 1 }, true },
            new object[] { new byte[1] { 0 }, false },
        };

        class PacketMock : Packet
        {
            public override byte PacketId => 255;

            public string TestReadStringFromStream(IPacketReader packetReader, Stream stream)
            {
                return ReadStringFromStream(packetReader, stream);
            }

            public Guid TestReadGuidFromStream(IPacketReader packetReader, Stream stream)
            {
                return ReadGuidFromStream(packetReader, stream);
            }

            public bool TestReadBooleanFromStream(IPacketReader packetReader, Stream stream)
            {
                return ReadBooleanFromStream(packetReader, stream);
            }
        }
    }
}
