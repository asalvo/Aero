using System;
using Aero.Cake.Features.DotNet.Services;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Core;

namespace Aero.Cake.Features.DotNet.Settings
{
    public static class PackSettings
    {
        /// <summary>
        /// Sets the following properties: Configuration, NoBuild, NoRestore,  MsBuildSettings (Uses MsBuildSettings.Default),
        ///   Additional Properties (Version, Copyright)
        /// </summary>
        public static DotNetCorePackSettings Default(VersionModel versionModel, string companyName, string configuration = "Release", bool noBuild = true)
        {
            //NoBuild is defaulted to true, because we should have built and restored already via the UnitTest task. 

            var msBuildSettings = MsBuildSettings.Default(versionModel);

            return new DotNetCorePackSettings
            {
                Configuration = configuration,
                NoBuild = noBuild,
                NoRestore = noBuild,
                MSBuildSettings = msBuildSettings,
                ArgumentCustomization = args => args
                    .Append($"/p:Version={versionModel.NuGetPackageVersion}")
                    .Append($"/p:Copyright=\"Copyright {DateTime.UtcNow.Year} {companyName}\"")
            };
        }
    }
}
