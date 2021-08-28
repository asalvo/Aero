using System;
using Aero.Cake.Services;
using Aero.Cake.WellKnown;
using FluentAssertions;
using FluentAssertions.Execution;
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
            AssertVersionModel(model, "1.2.3.4", "1.2.3.4", "1.2.3.4", "1.2.3+4");
        }

        [Fact]
        public void ParseVersion_From_Context_Throws_When_AppVersion_Does_Not_Exist()
        {
            //Act
            var exception = Assert.Throws<Exception>(() => ServiceUnderTest.ParseAppVersion());

            //Assert
            exception.Message.Should().Be("AppVersion argument missing");
        }

        [Theory]
        [InlineData("1.2.3.4", "1.2.3.4", "1.2.3.4", "1.2.3.4", "1.2.3+4")]
        [InlineData("100.2000.30000.400000", "100.2000.30000.400000", "100.2000.30000.400000", "100.2000.30000.400000", "100.2000.30000+400000")] //big numbers
        [InlineData("1.2.3.4-preview", "1.2.3.4", "1.2.3.4", "1.2.3.4-preview", "1.2.3-preview.4")]
        [InlineData("1.2.3.4-anythingAllowed", "1.2.3.4", "1.2.3.4", "1.2.3.4-anythingAllowed", "1.2.3-anythingAllowed.4")]
        public void ParseVersion_From_String_Parses_Correctly(string appVersion, string assemblyVersion, string fileVersion, string version, string nuGetPackageVersion)
        {
            //Act
            var model = ServiceUnderTest.ParseAppVersion(appVersion);

            //Assert
            AssertVersionModel(model, assemblyVersion, fileVersion, version, nuGetPackageVersion);
        }

        private void AssertVersionModel(VersionModel model, string assemblyVersion, string fileVersion, string version, string nuGetPackageVersion)
        {
            using (new AssertionScope())
            {
                model.AssemblyVersion.ToString().Should().Be(assemblyVersion);
                model.Version.Should().Be(version);
                model.FileVersion.ToString().Should().Be(fileVersion);
                model.NuGetPackageVersion.Should().Be(nuGetPackageVersion);
            }
        }
    }
}
