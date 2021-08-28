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
            //If you build during the test, the dependent projects will be rebuilt and you will loose version info

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
