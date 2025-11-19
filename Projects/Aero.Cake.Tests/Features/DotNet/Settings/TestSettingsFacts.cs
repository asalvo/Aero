using Cake.Common.Tools.DotNet.Test;
using Shouldly;
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
            testSettings.Configuration.ShouldBe("someConfiguration");
            testSettings.NoBuild.ShouldBeFalse();
            testSettings.NoRestore.ShouldBeFalse();
        }

        [Fact]
        public void SetNoBuildNoRestore_Sets_NoBuild_And_NoRestore_To_Specified_Value()
        {
            //Arrange
            var testSettings = new DotNetTestSettings();

            //Pre-Assert
            testSettings.NoBuild.ShouldBeFalse();
            testSettings.NoRestore.ShouldBeFalse();

            //Act
            testSettings.SetNoBuildNoRestore(true);

            //Assert
            testSettings.NoBuild.ShouldBeTrue();
            testSettings.NoRestore.ShouldBeTrue();
        }
    }
}
