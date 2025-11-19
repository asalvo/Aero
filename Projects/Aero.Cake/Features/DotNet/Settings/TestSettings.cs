using Cake.Common.Tools.DotNet.Test;

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
        public static DotNetTestSettings Default(string configuration = "Release", bool noBuild = false)
        {
            //If you build during the test, the dependent projects will be rebuilt and you will loose version info

            return new DotNetTestSettings
            {
                Configuration = configuration,
                Loggers = new[] { "trx" },
                NoBuild = noBuild,
                NoRestore = noBuild
            };
        }

        public static DotNetTestSettings SetNoBuildNoRestore(this DotNetTestSettings settings, bool noBuild)
        {
            settings.NoBuild = noBuild;
            settings.NoRestore = noBuild;
            return settings;
        }
    }
}
