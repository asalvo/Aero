using Cake.Common.Tools.DotNetCore.MSBuild;
using System.Text.RegularExpressions;

namespace Aero.Cake.Extensions
{
    public static class DotNetCoreMSBuildSettingsExtensions
    {
        /// <summary>
        /// Sets all three version properties, Version, AssemblyVersion and FileVersion.
        /// </summary>
        public static DotNetCoreMSBuildSettings SetAllVersions(this DotNetCoreMSBuildSettings settings, string version)
        {
            if(settings == null)
            {
                settings = new DotNetCoreMSBuildSettings();
            }

            version = ParseVersion(version);

            settings
                .WithProperty("Version", version)
                .WithProperty("AssemblyVersion", version)
                .WithProperty("FileVersion", version);

            return settings;
        }

        public static string ParseVersion(string version)
        {
            //This method exists and is public to support backwards compatibility with the VersionService which is now obsolete for DotNetCore projects

            const string semver = @"^(\d+\.\d+\.\d+)(?:-([0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*))?(?:\+([0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*))?$";
            var regex = new Regex(semver);
            var match = regex.Match(version);

            if (match.Success)
            {
                version = string.IsNullOrWhiteSpace(match.Groups[3].Value) ? match.Groups[1].Value : $"{match.Groups[1]}.{match.Groups[3]}";
            }
            else
            {
                //We either have Major.Minor.Build.Rev or yyMMdd.Rev from VSTS build
                //When we have yyMMdd.Rev, then we change it to yy.MM.dd.Rev
                var segments = version.Split('.');
                if (segments.Length == 2)
                {
                    version = $"{segments[0].Substring(0, 2)}.{segments[0].Substring(2, 2)}.{segments[0].Substring(4, 2)}.{segments[1]}";
                }
            }

            return version;
        }
    }
}
