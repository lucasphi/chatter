using Chatter.Worker.Network;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Chatter.UnitTests.Worker.Network
{
    public class PacketReaderTests
    {
        [Fact]
        public void ConvertByteArrayToInt()
        {
            var byteArray = new byte[4] { 5, 17, 211, 100 };
            var packetReader = new PacketReader();

            var result = packetReader.ConvertByteArrayToInt(byteArray);

            result.Should().Be(85054308);
        }

        [Fact]
        public void ThrowWhenConvertByteArrayToIntInvalidLenght()
        {
            var byteArray = new byte[3] { 5, 17, 211 };
            var packetReader = new PacketReader();

            Exception exception = Assert.Throws<ArgumentException>(() => packetReader.ConvertByteArrayToInt(byteArray));

            exception.Message.Should().Be("Byte array does not have Int length");
        }

        [Theory]
        [MemberData(nameof(BooleanBytes), MemberType = typeof(PacketReaderTests))]
        public void ConvertByteToBoolean(byte byteInput, bool expected)
        {
            var packetReader = new PacketReader();
             
            var result = packetReader.ConvertByteToBoolean(byteInput);

            result.Should().Be(expected);
        }

        [Fact]
        public void ConvertByteArrayToGuid()
        {
            var byteArray = new byte[16] { 98, 111, 189, 219, 189, 110, 44, 70, 168, 8, 141, 230, 172, 68, 49, 58 };
            var packetReader = new PacketReader();

            var result = packetReader.ConvertByteArrayToGuid(byteArray);

            result.Should().Be(Guid.Parse("dbbd6f62-6ebd-462c-a808-8de6ac44313a"));
        }

        [Fact]
        public void ThrowWhenConvertByteArrayToGuidInvalidLenght()
        {
            var byteArray = new byte[3] { 5, 17, 211 };
            var packetReader = new PacketReader();

            Exception exception = Assert.Throws<ArgumentException>(() => packetReader.ConvertByteArrayToGuid(byteArray));

            exception.Message.Should().Be("Byte array does not have Guid length");
        }

        public static IEnumerable<object[]> BooleanBytes => new List<object[]>
        {
            new object[] { 1, true },
            new object[] { 0, false },
            new object[] { 100, true }
        };
    }
}
