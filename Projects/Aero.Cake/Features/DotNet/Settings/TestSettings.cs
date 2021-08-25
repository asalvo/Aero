using Cake.Common.Tools.DotNetCore.Test;

namespace Aero.Cake.Features.DotNet.Settings
{
    public static class TestSettings
    {
        /// <summary>
        /// Sets the following properties:
        ///   - Configuration
        ///   - NoBuild
        ///   - NoRestore
        /// </summary>
        public static DotNetCoreTestSettings Default(string configuration = "Release", bool noBuild = false)
        {
            //We default noBuild to false, because even though in the majority of our builds (web, workers, etc)
            //we have a dedicated build task, that will only build the top level projects, not the test projects.

            return new DotNetCoreTestSettings
            {
                Configuration = configuration,
                Loggers = new[] { "trx" },
                NoBuild = noBuild,
                NoRestore = noBuild
            };
        }

        public static DotNetCoreTestSettings SetNoBuildNoRestore(this DotNetCoreTestSettings settings, bool noBuild)
        {
            settings.NoBuild = noBuild;
            settings.NoRestore = noBuild;
            return settings;
        }
    }
}
