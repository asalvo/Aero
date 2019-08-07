using Cake.Common.Tools.DotNetCore.MSBuild;
using FluentAssertions;
using Xunit;

namespace Aero.Cake.Extensions
{
    public class DotNetCoreMSBuildSettingsExtensionsFacts
    {
        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3.4", "1.2.3.4")]
        [InlineData("1.2.3+5", "1.2.3.5")]
        [InlineData("1.2.3-preview", "1.2.3")]
        [InlineData("1.2.3-preview+4", "1.2.3.4")]
        [InlineData("1.2.3-beta", "1.2.3")]
        [InlineData("1.2.3-preview.4", "1.2.3")]
        [InlineData("1.2.3-beta.4", "1.2.3")]
        [InlineData("1.2.3-beta.4+5", "1.2.3.5")]
        public void SetAllVersions_SemVer2_Theory(string providedVersion, string expectedVersion)
        {
            //Act
            var settings = new DotNetCoreMSBuildSettings().SetAllVersions(providedVersion);

            //Assert
            settings.Properties["Version"].Should().BeEquivalentTo(expectedVersion);
            settings.Properties["FileVersion"].Should().BeEquivalentTo(expectedVersion);
            settings.Properties["AssemblyVersion"].Should().BeEquivalentTo(expectedVersion);
        }

        [Fact]
        public void SetAllVersions_2_Segment_Test()
        {
            //Act
            var settings = new DotNetCoreMSBuildSettings().SetAllVersions("171126.1");

            //Assert
            settings.Properties["Version"].Should().BeEquivalentTo("17.11.26.1");
            settings.Properties["FileVersion"].Should().BeEquivalentTo("17.11.26.1");
            settings.Properties["AssemblyVersion"].Should().BeEquivalentTo("17.11.26.1");
        }
    }
}
