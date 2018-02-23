using FluentAssertions;
using Xunit;

namespace Aero.Common
{
    public class CryptoTests
    {
        private readonly Crypto _serviceUnderTest;

        public CryptoTests()
        {
            _serviceUnderTest = new Crypto();
        }

        [Fact]
        public void ValidatePasswordTest()
        {
            //Assert
            _serviceUnderTest.ValidatePassword("Test", "10000:HqulkQiYvrVO9ID6q8cZ6enTK0DbSB0n:VLqrhB/imGJzd1g5Fx3p7i5Vkno=").Should().BeTrue();
        }

        [Fact]
        public void HashPasswordTest()
        {
            //Arrange
            var password = "Test";
            var hash = _serviceUnderTest.HashPassword(password);

            //Assert
            _serviceUnderTest.ValidatePassword(password, hash).Should().BeTrue();
        }


    }
}
