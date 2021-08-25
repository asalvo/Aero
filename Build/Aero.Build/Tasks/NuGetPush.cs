using Aero.Build.WellKnown;
using Aero.Cake.Extensions;
using Aero.Cake.Features.DotNet.Settings;
using Aero.Cake.Features.DotNet.Wrappers;
using Aero.Cake.WellKnown;
using Cake.Common;
using Cake.Common.Tools.DotNetCore.NuGet.Push;
using Cake.Core.IO;
using Cake.Frosting;

namespace Aero.Build.Tasks
{
    public class NuGetPush : FrostingTask<MyContext>
    {
        private readonly IDotNetCoreWrapper _dotNetCore;

        public NuGetPush(IDotNetCoreWrapper dotNetCore)
        {
            _dotNetCore = dotNetCore;
        }

        public override void Run(MyContext context)
        {
            var appVersion = context.Argument<string>(ArgumentNames.AppVersion);
            var nuGetApiPassword = context.Argument<string>(ArgumentNames.NuGet.ApiKey);
            var nuGetSource = context.Argument(ArgumentNames.NuGet.Source, "https://api.nuget.org/v3/index.json");

            var settings = NuGetPushSettings.Default(nuGetApiPassword, nuGetSource);

            appVersion = appVersion.ParseVersionForNuPkg();

            PushAero(context, appVersion, settings);
            PushAeroCake(context, appVersion, settings);
            PushAeroCakeTestSupport(context, appVersion, settings);
        }

        private void PushAero(MyContext context, string appVersion, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.Aero}/bin/{context.BuildConfiguration}/{Projects.Aero}.{appVersion}.nupkg");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }

        private void PushAeroCake(MyContext context, string appVersion, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroCake}/bin/{context.BuildConfiguration}/{Projects.AeroCake}.{appVersion}.nupkg");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }

        private void PushAeroCakeTestSupport(MyContext context, string appVersion, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroCakeTestSupport}/bin/{context.BuildConfiguration}/{Projects.AeroCakeTestSupport}.{appVersion}.nupkg");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }
    }
}
