using System;
using System.Text.RegularExpressions;
using Aero.Cake.Extensions;
using Aero.Cake.Services;
using Cake.Common;
using Cake.Common.Diagnostics;
using NuGet.Versioning;

namespace Aero.Cake.Features.DotNet.Services
{
    public interface IVersionService
    {
        /// <summary>
        /// Takes a 4 segment version number with optional "-preview" suffix and converts it into all of the
        /// version number/strings that we need for Assemblies and NuGet packages. The 1.2.3.4-preview format
        /// can easily be set in TeamCity using the BuildNumber field and optional parameters
        /// </summary>
        /// <param name="appVersion"></param>
        VersionModel ParseAppVersion(string appVersion);

        /// <summary>
        /// Convenience method which allows you to parse the appVersion from the service's IMyContext saving you from having to
        /// get the argument value before calling ParseAppversion.
        /// </summary>
        VersionModel ParseAppVersion();
    }

    /// <summary>
    /// Implements IVersionService, register as a singleton
    /// </summary>
    public class VersionService : AbstractService, IVersionService
    {
        public const string AppVersionRegEx = @"([0-9]+)\.([0-9]+)\.([0-9]+)\.([0-9]+)(-[a-zA-Z]+)*";

        public VersionService(IAeroContext myContext) : base(myContext)
        {
        }

        public VersionModel ParseAppVersion(string appVersion)
        {
            AeroContext.Information($"VersionService.ParseAppVersion. Action: Start, AppVersion: {appVersion}");

            var model = new VersionModel();

            var match = new Regex(AppVersionRegEx).Match(appVersion);

            //Group 0 is the full match, 1-4 are the parts and 5 is the -preview
            if(!match.Success || match.Groups.Count < 4 || match.Groups.Count > 6)
            {
                AeroContext.Error($"VersionService.ParseAppVersion. Action: RegExFail, AppVersion: {appVersion}, GroupCount: {match.Groups?.Count}");
                throw new Exception("AppVersion RegEx Failed");
            }

            try
            {
                model.AssemblyVersion = new Version(
                    Convert.ToInt32(match.Groups[1].Value),
                    Convert.ToInt32(match.Groups[2].Value),
                    Convert.ToInt32(match.Groups[3].Value),
                    Convert.ToInt32(match.Groups[4].Value)
                );
            }
            catch(Exception ex)
            {
                AeroContext.Error($"VersionService.ParseAppVersion. Action: VersionObjectCreationFailed, AppVersion: {appVersion}, {ex.ToLogString()}");
                throw;
            }

            //If we have a valid regex and we successfully created a version object then we can just set the Version to the appVersion and it will
            //work for either 1.2.3.4 or 1.2.3.4-preview
            model.Version = appVersion;

            if(match.Groups.Count == 6 && !string.IsNullOrWhiteSpace(match.Groups[5].Value))
            {
                var versionSuffix = $"{match.Groups[5]}.{model.AssemblyVersion.Revision}";
                model.NuGetPackageVersion = $"{model.AssemblyVersion.Major}.{model.AssemblyVersion.Minor}.{model.AssemblyVersion.Build}{versionSuffix}";
                model.NuGetFileName = model.NuGetPackageVersion;
            }
            else
            {
                model.NuGetPackageVersion = $"{model.AssemblyVersion.Major}.{model.AssemblyVersion.Minor}.{model.AssemblyVersion.Build}+{model.AssemblyVersion.Revision}";
                model.NuGetFileName = $"{model.AssemblyVersion.Major}.{model.AssemblyVersion.Minor}.{model.AssemblyVersion.Build}";
            }

            AeroContext.Information($"VersionService.ParseAppVersion. Action: Stop, AppVersion: {appVersion}, {model.ToLogString()}");
            return model;
        }

        public VersionModel ParseAppVersion()
        {
            //Accept a default value of empty string and then explicitly check so we can provide a better error message
            var appVersion = AeroContext.Argument(Cake.WellKnown.ArgumentNames.AppVersion, string.Empty);

            if (string.IsNullOrWhiteSpace(appVersion))
            {
                AeroContext.Error($"VersionService.ParseAppVersion. Action: AppVersionNull, Message: This method requires a context with  an argument named {nameof(Cake.WellKnown.ArgumentNames.AppVersion)}");
                throw new Exception("AppVersion argument missing");
            }

            return ParseAppVersion(appVersion);
        }
    }

    public class VersionModel
    {
        public VersionModel()
        {
            AssemblyVersion = new Version();
        }

        public Version AssemblyVersion { get; set; }

        public Version FileVersion => AssemblyVersion;

        public string Version { get; set; }

        public string NuGetFileName { get; set; }

        public string NuGetPackageVersion { get; set; }

        public string ToLogString() => $"AssemblyVersion: {AssemblyVersion}, FileVersion: {FileVersion}, Version: {Version}, NuGetPackageVersion: {NuGetPackageVersion}";
    }
}
