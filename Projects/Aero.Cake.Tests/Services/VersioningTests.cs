using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Testing;
using FluentAssertions;
using NSubstitute;
using Xunit;
using CakeCore = Cake.Core;

namespace Aero.Cake.Services
{
    public class VersioningTests : ServiceFixture<VersionService>
    {
        public VersioningTests()
        {
            ServiceUnderTest = new VersionService(Context, Logger);
        }

        [Fact]
        public void Update_Test()
        {
            //Arrange
            CakeContext.Globber.Match($"{Context.ProjectsPath}/**/*.csproj").Returns(new List<FilePath> {"/p1/p1.csproj"});

            CakeContext.FileSystem.CreateFile("/p1/p1.csproj", ReadFile($"{TestDirectory}/TestFiles/VersionAttributeExists.csproj"));

            //Act
            ServiceUnderTest.UpdateFiles("2.2.2.2", "../projects");

            //Assert
            var content = CakeContext.FileSystem.GetFile("/p1/p1.csproj").Content;
            System.Text.Encoding.ASCII.GetString(content).Should().Contain("<AssemblyVersion>2.2.2.2</AssemblyVersion>");
            System.Text.Encoding.ASCII.GetString(content).Should().Contain("<FileVersion>2.2.2.2</FileVersion>");
            System.Text.Encoding.ASCII.GetString(content).Should().Contain("<Version>2.2.2.2</Version>");
        }

        [Fact]
        public void Update_2_Segment_Test()
        {
            //Arrange
            CakeContext.Globber.Match($"{Context.ProjectsPath}/**/*.csproj").Returns(new List<FilePath> { "/p1/p1.csproj" });

            CakeContext.FileSystem.CreateFile("/p1/p1.csproj", ReadFile($"{TestDirectory}/TestFiles/VersionAttributeExists.csproj"));

            //Act
            ServiceUnderTest.UpdateFiles("171126.1", "../projects");

            //Assert
            var content = CakeContext.FileSystem.GetFile("/p1/p1.csproj").Content;
            System.Text.Encoding.ASCII.GetString(content).Should().Contain("<AssemblyVersion>17.11.26.1</AssemblyVersion>");
            System.Text.Encoding.ASCII.GetString(content).Should().Contain("<FileVersion>17.11.26.1</FileVersion>");
            System.Text.Encoding.ASCII.GetString(content).Should().Contain("<Version>17.11.26.1</Version>");
        }

        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3+5", "1.2.3.5")]
        [InlineData("1.2.3-preview", "1.2.3")]
        [InlineData("1.2.3-preview+4", "1.2.3.4")]
        [InlineData("1.2.3-beta", "1.2.3")]
        [InlineData("1.2.3-preview.4", "1.2.3")]
        [InlineData("1.2.3-beta.4", "1.2.3")]
        [InlineData("1.2.3-beta.4+5", "1.2.3.5")]
        public void Update_SemVer2_Test(string providedVersion, string expectedVersion)
        {
            //Arrange
            CakeContext.Globber.Match($"{Context.ProjectsPath}/**/*.csproj").Returns(new List<FilePath> { "/p1/p1.csproj" });

            CakeContext.FileSystem.CreateFile("/p1/p1.csproj", ReadFile($"{TestDirectory}/TestFiles/VersionAttributeExists.csproj"));

            //Act
            ServiceUnderTest.UpdateFiles(providedVersion, "../projects");

            //Assert
            var content = CakeContext.FileSystem.GetFile("/p1/p1.csproj").Content;
            System.Text.Encoding.ASCII.GetString(content).Should().Contain($"<AssemblyVersion>{expectedVersion}</AssemblyVersion>");
            System.Text.Encoding.ASCII.GetString(content).Should().Contain($"<FileVersion>{expectedVersion}</FileVersion>");
            System.Text.Encoding.ASCII.GetString(content).Should().Contain($"<Version>{expectedVersion}</Version>");
        }

        [Fact]
        public void Update_When_Attribute_Missing_Exception_Is_Throw()
        {
            //Arrange
            CakeContext.Globber.Match($"{Context.ProjectsPath}/**/*.csproj").Returns(new List<FilePath> { "/p2/p2.csproj" });

            CakeContext.FileSystem.CreateFile("/p2/p2.csproj", ReadFile($"{TestDirectory}/TestFiles/VersionAttributeDoesNotExist.csproj"));

            //Act
            Assert.Throws<CakeCore.CakeException>(() => ServiceUnderTest.UpdateFiles("2.2.2.2", "../projects"));

        }

        [Fact]
        public void Update_Exclude_Files_Test()
        {
            //Arrange
            CakeContext.Globber.Match($"{Context.ProjectsPath}/**/*.csproj").Returns(new List<FilePath> { "/p1/p1.csproj", "/p2/p2.csproj" });

            CakeContext.FileSystem.CreateFile("/p1/p1.csproj", ReadFile($"{TestDirectory}/TestFiles/VersionAttributeExists.csproj"));
            CakeContext.FileSystem.CreateFile("/p2/p2.csproj", ReadFile($"{TestDirectory}/TestFiles/VersionAttributeDoesNotExist.csproj"));

            //Act
            ServiceUnderTest.UpdateFiles("2.2.2.2", "../projects", "P2"); //pass P2 with setup above of p2 to test case insensitivity

        }
    }
}
