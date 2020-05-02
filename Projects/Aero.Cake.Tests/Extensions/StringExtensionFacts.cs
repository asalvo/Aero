using FluentAssertions;
using Xunit;

namespace Aero.Cake.Extensions
{
    public class StringExtensionFacts
    {
        [Theory]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3.4", "1.2.3.4")]
        [InlineData("1.2.3+5", "1.2.3")] //the +5 is dropped
        [InlineData("1.2.3-preview", "1.2.3-preview")]
        [InlineData("1.2.3-preview+4", "1.2.3-preview")] //+4 is dropped
        [InlineData("1.2.3-beta", "1.2.3-beta")]
        [InlineData("1.2.3-preview.4", "1.2.3-preview.4")]
        [InlineData("1.2.3-beta.4", "1.2.3-beta.4")]
        [InlineData("1.2.3-beta.4+5", "1.2.3-beta.4")] //+5 is dropped
        public void ParseVersionForNuSpec_Theory(string providedVersion, string expectedVersion)
        {
            //Act
            var version = providedVersion.ParseVersionForNuPkg();

            //Assert
            version.Should().BeEquivalentTo(expectedVersion);
        }
    }
}
