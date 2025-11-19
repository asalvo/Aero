using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Aero.Cake.Features.DotNet.Settings
{
    public class MsBuildSettingsFacts
    {
        [Fact]
        public void DefaultSettings_Are_As_Expected()
        {
            //Arrange
            var versionModel = new Services.VersionModel() { AssemblyVersion = new Version("1.2.3.4"), NuGetPackageVersion = "1.2.3+4", Version = "1.2.3.4" };

            //Act
            var msBuildSettings = MsBuildSettings.Default(versionModel);

            //Assert
            msBuildSettings.Properties.Count.ShouldBe(3);
            msBuildSettings.Properties["Version"].ShouldBe(new List<string>{ "1.2.3.4" });
            msBuildSettings.Properties["AssemblyVersion"].ShouldBe(new List<string>{ "1.2.3.4" });
            msBuildSettings.Properties["FileVersion"].ShouldBe(new List<string>{ "1.2.3.4" });
        }
    }
}
