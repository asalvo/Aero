using System.Security.Cryptography;
using Shouldly;
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
        public void HashPassword_1000_Test()
        {
            //Arrange
            var password = "Test";
            var hash = _serviceUnderTest.HashPassword(password);

            //Assert
            _serviceUnderTest.ValidatePassword(password, hash).ShouldBeTrue();
        }

        [Fact]
        public void HashPassword_1001_WithSamePassword_ShouldProduceDifferentHashes()
        {
            //Arrange
            var password = "Test";

            //Act
            var hash1 = _serviceUnderTest.HashPassword(password);
            var hash2 = _serviceUnderTest.HashPassword(password);

            //Assert
            hash1.ShouldNotBe(hash2);
            _serviceUnderTest.ValidatePassword(password, hash1).ShouldBeTrue();
            _serviceUnderTest.ValidatePassword(password, hash2).ShouldBeTrue();
        }

        [Fact]
        public void HashPassword_1002_WithDifferentPasswords_ShouldProduceDifferentHashes()
        {
            //Arrange
            var password1 = "Test1";
            var password2 = "Test2";

            //Act
            var hash1 = _serviceUnderTest.HashPassword(password1);
            var hash2 = _serviceUnderTest.HashPassword(password2);

            //Assert
            hash1.ShouldNotBe(hash2);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10000)]
        [InlineData(100000)]
        [InlineData(1000000)]
        public void HashPassword_1003_WithCustomIterations_ShouldUseSpecifiedIterations(int iterations)
        {
            //Arrange
            var password = "Test";

            //Act
            var hash = _serviceUnderTest.HashPassword(password, iterations, HashAlgorithmName.SHA256);

            //Assert
            hash.ShouldStartWith($"{iterations}:");
            _serviceUnderTest.ValidatePassword(password, hash, HashAlgorithmName.SHA256).ShouldBeTrue();
        }

        [Fact]
        public void HashPassword_1004_WithDifferentAlgorithms_ShouldProduceDifferentHashes()
        {
            //Arrange
            var password = "Test";
            var iterations = 50000;

            //Act
            var hashSha1 = _serviceUnderTest.HashPassword(password, iterations, HashAlgorithmName.SHA1);
            var hashSha256 = _serviceUnderTest.HashPassword(password, iterations, HashAlgorithmName.SHA256);
            var hashSha512 = _serviceUnderTest.HashPassword(password, iterations, HashAlgorithmName.SHA512);

            //Assert
            hashSha1.ShouldNotBe(hashSha256);
            hashSha256.ShouldNotBe(hashSha512);
            hashSha1.ShouldNotBe(hashSha512);
        }

        [Fact]
        public void ValidatePassword_1000_Sha1_10000()
        {
            //Assert - This hash was created with SHA1 and 10000 iterations
            _serviceUnderTest.ValidatePassword("Test", "10000:HqulkQiYvrVO9ID6q8cZ6enTK0DbSB0n:VLqrhB/imGJzd1g5Fx3p7i5Vkno=", HashAlgorithmName.SHA1).ShouldBeTrue();
        }

        [Fact]
        public void ValidatePassword_1001_Sha512_300000()
        {
            //Assert - This hash was created with SHA512 and 300000 iterations
            _serviceUnderTest.ValidatePassword("Test", "300000:HqulkQiYvrVO9ID6q8cZ6enTK0DbSB0n:wkh8lHBHtXCEHBJGVWW2doJKS7I=", HashAlgorithmName.SHA512).ShouldBeTrue();
        }

        [Fact]
        public void ValidatePassword_1002_Sha256_100000()
        {
            //Assert - This hash was created with SHA256 and 100000 iterations
            _serviceUnderTest.ValidatePassword("Test", "100000:HqulkQiYvrVO9ID6q8cZ6enTK0DbSB0n:gnp9agndPT+A26Dz0WmCUf2O1Ag=", HashAlgorithmName.SHA256).ShouldBeTrue();
        }

        [Fact]
        public void ValidatePassword_1003_DefaultParameters_Sha512_300000()
        {
            //Arrange - Test that default parameters use SHA512 and 300000
            var password = "TestDefault";
            var hash = _serviceUnderTest.HashPassword(password);

            //Assert - Should validate with explicit SHA512 and 300000
            _serviceUnderTest.ValidatePassword(password, hash,HashAlgorithmName.SHA512).ShouldBeTrue();
            hash.ShouldStartWith("300000:");
        }

        [Fact]
        public void ValidatePassword_1010_WithIncorrectPassword_ShouldReturnFalse()
        {
            //Arrange
            var correctPassword = "Test";
            var incorrectPassword = "Wrong";
            var hash = _serviceUnderTest.HashPassword(correctPassword);

            //Assert
            _serviceUnderTest.ValidatePassword(incorrectPassword, hash).ShouldBeFalse();
        }
    }
}
