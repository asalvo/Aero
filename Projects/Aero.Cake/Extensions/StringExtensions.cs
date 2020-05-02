using System.Text.RegularExpressions;

namespace Aero.Cake.Extensions
{
    public static class StringExtensions
    {
        public static string ParseVersion(this string version)
        {
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

        public static string ParseVersionForNuPkg(this string version)
        {
            const string semver = @"^(\d+\.\d+\.\d+)(?:-([0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*))?(?:\+([0-9A-Za-z-]+(?:\.[0-9A-Za-z-]+)*))?$";
            var regex = new Regex(semver);
            var match = regex.Match(version);

            if (match.Success)
            {
                version = string.IsNullOrWhiteSpace(match.Groups[2].Value) ? $"{match.Groups[1]}" : $"{match.Groups[1]}-{match.Groups[2]}";
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
