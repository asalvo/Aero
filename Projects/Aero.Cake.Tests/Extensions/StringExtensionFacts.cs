using FluentAssertions;
using Xunit;

namespace Aero.Cake.Extensions
{
    public class StringExtensionFacts
    {
        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3.4", "1.2.3.4")]
        [InlineData("1.2.3+5", "1.2.3.5")]
        [InlineData("1.2.3-preview", "1.2.3")]
        [InlineData("1.2.3-preview+4", "1.2.3.4")]
        [InlineData("1.2.3-beta", "1.2.3")]
        [InlineData("1.2.3-preview.4", "1.2.3-preview.4")]
        [InlineData("1.2.3-beta.4", "1.2.3")]
        [InlineData("1.2.3-beta.4+5", "1.2.3.5")]
        public void SetAllVersions_SemVer2_Theory(string providedVersion, string expectedVersion)
        {
            //Act
            var version = providedVersion.ParseVersionForNuSpec();

            //Assert
            version.Should().BeEquivalentTo(expectedVersion);
        }
    }
}
