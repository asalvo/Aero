using Aero.Build.WellKnown;
using Aero.Cake.Extensions;
using Aero.Cake.Services;
using Cake.Common;
using Cake.Common.Tools.DotNetCore.NuGet.Push;
using Cake.Core.IO;
using Cake.Frosting;

namespace Aero.Build.Tasks
{
    public class NuGetPush : FrostingTask<MyContext>
    {
        private readonly IDotNetCoreService _dotNetCore;

        public NuGetPush(IDotNetCoreService dotNetCore)
        {
            _dotNetCore = dotNetCore;
        }

        public override void Run(MyContext context)
        {
            var appVersion = context.Argument<string>(ArgumentNames.AppVersion);
            var nuGetFeedPassword = context.Argument<string>(ArgumentNames.NuGetFeedPassword);
            var nuGetFeedUrl = context.Argument(ArgumentNames.NuGetFeedUrl, "https://api.nuget.org/v3/index.json");

            var settings = new DotNetCoreNuGetPushSettings
            {
                ApiKey = nuGetFeedPassword,
                Source = nuGetFeedUrl
            };

            appVersion = appVersion.ParseVersionForNuPkg();

            PushAero(context, appVersion, settings);
            PushAeroAzure(context, appVersion, settings);
            PushAeroCake(context, appVersion, settings);
        }

        private void PushAero(MyContext context, string appVersion, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.Aero}/bin/{context.BuildConfiguration}/{Projects.Aero}.{appVersion}.nupkg");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }

        private void PushAeroAzure(MyContext context, string appVersion, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroAzure}/bin/{context.BuildConfiguration}/{Projects.AeroAzure}.{appVersion}.nupkg");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }

        private void PushAeroCake(MyContext context, string appVersion, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroCake}/bin/{context.BuildConfiguration}/{Projects.AeroCake}.{appVersion}.nupkg");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }
    }
}
