using System;
using System.Collections.Generic;
using FluentAssertions;
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
            packSettings.Configuration.Should().Be("someConfiguration");
            packSettings.NoBuild.Should().BeFalse();
            packSettings.NoRestore.Should().BeFalse();

            var customPackSettings = packSettings.ArgumentCustomization(new global::Cake.Core.IO.ProcessArgumentBuilder());
            customPackSettings.Render().Should().Be($"/p:Version=1.2.3+4 /p:Copyright=\"Copyright {DateTime.UtcNow.Year} SomeCompany\"");

            packSettings.MSBuildSettings.Properties.Should().BeEquivalentTo(new Dictionary<string, ICollection<string>>()
                {
                    {"Version", new List<string>{ "1.2.3.4" } },
                    {"AssemblyVersion", new List<string>{ "1.2.3.4" } },
                    {"FileVersion", new List<string>{ "1.2.3.4" } }
                });
        }
    }
}
