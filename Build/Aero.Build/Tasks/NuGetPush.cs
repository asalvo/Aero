using Aero.Build.WellKnown;
using Aero.Cake.Extensions;
using Aero.Cake.Features.DotNet.Services;
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
        private readonly IVersionService _versionService;

        public NuGetPush(IDotNetCoreWrapper dotNetCore, IVersionService versionService)
        {
            _dotNetCore = dotNetCore;
            _versionService = versionService;
        }

        public override void Run(MyContext context)
        {
            var nuGetApiPassword = context.Argument<string>(ArgumentNames.NuGet.ApiKey);
            var nuGetSource = context.Argument(ArgumentNames.NuGet.Source, "https://api.nuget.org/v3/index.json");

            var settings = NuGetPushSettings.Default(nuGetApiPassword, nuGetSource);
            var versionModel = _versionService.ParseAppVersion();

            PushAero(context, versionModel.NuGetPackageVersion, settings);
            PushAeroCake(context, versionModel.NuGetPackageVersion, settings);
            PushAeroCakeTestSupport(context, versionModel.NuGetPackageVersion, settings);
        }

        private void PushAero(MyContext context, string nuGetPackageVersion, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.Aero}/bin/{context.BuildConfiguration}/{Projects.Aero}.{nuGetPackageVersion}.nupkg");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }

        private void PushAeroCake(MyContext context, string nuGetPackageVersion, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroCake}/bin/{context.BuildConfiguration}/{Projects.AeroCake}.{nuGetPackageVersion}.nupkg");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }

        private void PushAeroCakeTestSupport(MyContext context, string nuGetPackageVersion, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroCakeTestSupport}/bin/{context.BuildConfiguration}/{Projects.AeroCakeTestSupport}.{nuGetPackageVersion}.nupkg");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }
    }
}
