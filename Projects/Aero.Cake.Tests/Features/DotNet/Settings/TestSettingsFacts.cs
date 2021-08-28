using Cake.Common.Tools.DotNetCore.Test;
using FluentAssertions;
using Xunit;

namespace Aero.Cake.Features.DotNet.Settings
{
    public class TestSettingsFacts
    {
        [Fact]
        public void DefaultSettings_Are_As_Expected()
        {
            //Act
            var testSettings = TestSettings.Default("someConfiguration", false);

            //Assert
            testSettings.Configuration.Should().Be("someConfiguration");
            testSettings.NoBuild.Should().BeFalse();
            testSettings.NoRestore.Should().BeFalse();
        }

        [Fact]
        public void SetNoBuildNoRestore_Sets_NoBuild_And_NoRestore_To_Specified_Value()
        {
            //Arrange
            var testSettings = new DotNetCoreTestSettings();

            //Pre-Assert
            testSettings.NoBuild.Should().BeFalse();
            testSettings.NoRestore.Should().BeFalse();

            //Act
            testSettings.SetNoBuildNoRestore(true);

            //Assert
            testSettings.NoBuild.Should().BeTrue();
            testSettings.NoRestore.Should().BeTrue();
        }
    }
}
