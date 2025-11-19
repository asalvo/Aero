using System;
using Aero.Cake.Services;
using Aero.Cake.WellKnown;
using Shouldly;
using Xunit;

namespace Aero.Cake.Features.DotNet.Services
{
    public class VersionServiceFacts : ServiceFixture<VersionService>
    {
        public VersionServiceFacts()
        {
            ServiceUnderTest = new VersionService(MyContext);
        }

        [Fact]
        public void ParseVersion_From_Context_Returns_Version()
        {
            //Arrange
            MockContext.Arguments.AddArgument(ArgumentNames.AppVersion, "1.2.3.4");

            //Act
            var model = ServiceUnderTest.ParseAppVersion();

            //Assert
            AssertVersionModel(model, "1.2.3.4", "1.2.3.4", "1.2.3.4", "1.2.3+4", "1.2.3"); // .NET 10: revision stripped
        }

        [Fact]
        public void ParseVersion_From_Context_Throws_When_AppVersion_Does_Not_Exist()
        {
            //Act
            var exception = Assert.Throws<Exception>(() => ServiceUnderTest.ParseAppVersion());

            //Assert
            exception.Message.ShouldBe("AppVersion argument missing");
        }

        [Theory]
        [InlineData("1.2.3.4", "1.2.3.4", "1.2.3.4", "1.2.3.4", "1.2.3+4", "1.2.3")] // .NET 10: revision stripped from filename
        [InlineData("1.0.1.0", "1.0.1.0", "1.0.1.0", "1.0.1.0", "1.0.1+0", "1.0.1")] // .NET 10: trailing .0 stripped
        [InlineData("1.2.3.4-preview", "1.2.3.4", "1.2.3.4", "1.2.3.4-preview", "1.2.3-preview.4", "1.2.3-preview.4")] // .NET 10: build number after prerelease tag
        [InlineData("1.2.3.4-anythingAllowed", "1.2.3.4", "1.2.3.4", "1.2.3.4-anythingAllowed", "1.2.3-anythingAllowed.4", "1.2.3-anythingAllowed.4")] // .NET 10: build number after prerelease tag
        [InlineData("4.0.0.7-preview", "4.0.0.7", "4.0.0.7", "4.0.0.7-preview", "4.0.0-preview.7", "4.0.0-preview.7")] // .NET 10: additional prerelease test
        public void ParseVersion_From_String_Parses_Correctly(string appVersion, string assemblyVersion, string fileVersion, string version, string nuGetPackageVersion, string nuGetFileName)
        {
            //Act
            var model = ServiceUnderTest.ParseAppVersion(appVersion);

            //Assert
            AssertVersionModel(model, assemblyVersion, fileVersion, version, nuGetPackageVersion, nuGetFileName);
        }

        private void AssertVersionModel(VersionModel model, string assemblyVersion, string fileVersion, string version, string nuGetPackageVersion, string nuGetFilename)
        {
            model.AssemblyVersion.ToString().ShouldBe(assemblyVersion);
            model.Version.ShouldBe(version);
            model.FileVersion.ToString().ShouldBe(fileVersion);
            model.NuGetPackageVersion.ShouldBe(nuGetPackageVersion);
            model.NuGetFileName.ShouldBe(nuGetFilename);
        }
    }
}
