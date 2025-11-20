using Aero.Cake.Features.DotNet.Services;
using Cake.Common.Tools.DotNet.Publish;

namespace Aero.Cake.Features.DotNet.Settings
{
    public static class PublishSettings
    {
        /// <summary>
        /// Creates default publish settings for a folder publish operation. This builds for all runtimes and does not include DotNetCore.
        /// Sets the following properties: Configuration, NoBuild, NoRestore,  MsBuildSettings (Uses MsBuildSettings.Default),
        ///   Additional Properties (Version, Copyright)
        /// </summary>
        /// <remarks>
        /// - It is assumed that publish is called from a Build Task in which build was not called first, so NoBuild is set to false by default.
        /// </remarks>
        public static DotNetPublishSettings Default(VersionModel versionModel, string configuration = "Release", bool noBuild = false)
        {
            var msBuildSettings = MsBuildSettings.Default(versionModel);

            return new DotNetPublishSettings
            {
                Configuration = configuration,
                MSBuildSettings = msBuildSettings,
                NoBuild = noBuild,
                NoRestore = noBuild
            };
        }
    }
}
