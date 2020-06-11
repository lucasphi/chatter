using Chatter.Worker.Network;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Chatter.UnitTests.Worker.Network
{
    public class PacketWriterTests
    {
        [Fact]
        public void WriteByte()
        {
            var packetWriter = new PacketWriter();
            byte value = 200;

            packetWriter.WriteByte(value);
            var result = packetWriter.GetBytes();

            result.Should().BeEquivalentTo(new byte[1] { 200 });
        }

        [Fact]
        public void WriteInt()
        {
            var packetWriter = new PacketWriter();
            var value = 200;

            packetWriter.WriteInt(value);
            var result = packetWriter.GetBytes();

            result.Should().BeEquivalentTo(new byte[4] { 0, 0, 0, 200 });
        }

        [Theory]
        [MemberData(nameof(BooleanBytes), MemberType = typeof(PacketWriterTests))]
        public void WriteBool(bool value, byte[] expected)
        {
            var packetWriter = new PacketWriter();

            packetWriter.WriteBool(value);
            var result = packetWriter.GetBytes();

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void WriteString()
        {
            var packetWriter = new PacketWriter();
            var value = "This is an awesome test!";

            packetWriter.WriteString(value);
            var result = packetWriter.GetBytes();

            result.Should().BeEquivalentTo(new byte[28] { 0, 0, 0, 24, 84, 104, 105, 115, 32, 105, 115, 32, 97, 110, 32, 97, 119, 101, 115, 111, 109, 101, 32, 116, 101, 115, 116, 33 });
        }

        [Fact]
        public void WriteGuid()
        {
            var packetWriter = new PacketWriter();
            var value = Guid.Parse("dbbd6f62-6ebd-462c-a808-8de6ac44313a");

            packetWriter.WriteGuid(value);
            var result = packetWriter.GetBytes();

            result.Should().BeEquivalentTo(new byte[16] { 98, 111, 189, 219, 189, 110, 44, 70, 168, 8, 141, 230, 172, 68, 49, 58 });
        }

        public static IEnumerable<object[]> BooleanBytes => new List<object[]>
        {
            new object[] { true, new byte[1] { 1 } },
            new object[] { false, new byte[1] { 0 } },
        };
    }
}
