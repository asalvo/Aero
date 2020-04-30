using Aero.Build.WellKnown;
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
            var nuGetFeedPassword = context.Argument<string>(ArgumentNames.NuGetFeedPassword);
            var nuGetFeedUrl = context.Argument(ArgumentNames.NuGetFeedUrl, "https://api.nuget.org/v3/index.json");

            var settings = new DotNetCoreNuGetPushSettings
            {
                ApiKey = nuGetFeedPassword,
                Source = nuGetFeedUrl
            };
            
            PushAero(context, settings);
            PushAeroAzure(context, settings);
            PushAeroCake(context, settings);
        }

        private void PushAero(MyContext context, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.Aero}/{Projects.Aero}.csproj");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }

        private void PushAeroAzure(MyContext context, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroAzure}/{Projects.AeroAzure}.csproj");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }

        private void PushAeroCake(MyContext context, DotNetCoreNuGetPushSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroCake}/{Projects.AeroCake}.csproj");
            _dotNetCore.NuGetPush(path.FullPath, defaultSettings);
        }
    }
}
