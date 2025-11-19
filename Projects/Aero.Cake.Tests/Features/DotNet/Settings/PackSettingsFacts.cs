using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Aero.Cake.Features.DotNet.Settings
{
    public class PackSettingsFacts
    {
        [Fact]
        public void DefaultSettings_Are_As_Expected()
        {
            //Arrange
            var versionModel = new Services.VersionModel() { AssemblyVersion = new Version("1.2.3.4"), NuGetPackageVersion = "1.2.3+4", Version = "1.2.3.4" };

            //Act
            var packSettings = PackSettings.Default(versionModel, "SomeCompany", "someConfiguration", false);

            //Assert
            packSettings.Configuration.ShouldBe("someConfiguration");
            packSettings.NoBuild.ShouldBeFalse();
            packSettings.NoRestore.ShouldBeFalse();

            var customPackSettings = packSettings.ArgumentCustomization(new global::Cake.Core.IO.ProcessArgumentBuilder());
            customPackSettings.Render().ShouldBe($"/p:Version=1.2.3+4 /p:Copyright=\"Copyright {DateTime.UtcNow.Year} SomeCompany\"");

            packSettings.MSBuildSettings.Properties.Count.ShouldBe(3);
            packSettings.MSBuildSettings.Properties["Version"].ShouldBe(new List<string>{ "1.2.3.4" });
            packSettings.MSBuildSettings.Properties["AssemblyVersion"].ShouldBe(new List<string>{ "1.2.3.4" });
            packSettings.MSBuildSettings.Properties["FileVersion"].ShouldBe(new List<string>{ "1.2.3.4" });
        }
    }
}
