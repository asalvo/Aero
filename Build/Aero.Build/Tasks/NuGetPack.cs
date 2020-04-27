using System;
using Aero.Build.WellKnown;
using Aero.Cake.Services;
using Cake.Common;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

namespace Aero.Build.Tasks
{
    public class NuGetPack : FrostingTask<MyContext>
    {
        private readonly IDotNetCoreService _dotNetCore;

        public NuGetPack(IDotNetCoreService dotNetCore)
        {
            _dotNetCore = dotNetCore;
        }

        public override void Run(MyContext context)
        {
            var appVersion = context.Argument<string>(ArgumentNames.AppVersion);
            
            //https://github.com/NuGet/Home/wiki/Adding-nuget-pack-as-a-msbuild-target
            var settings = new DotNetCorePackSettings
            {
                Configuration = context.BuildConfiguration,
                NoBuild = true,
                ArgumentCustomization = args => args
                    .Append($"/p:Version={appVersion}")
                    .Append($"/p:Copyright=\"Copyright {DateTime.UtcNow.Year} Adam Salvo\"")
            };
            
            PackAero(context, settings);
            PackAeroAzure(context, settings);
            PackAeroCake(context, settings);
        }

        private void PackAero(MyContext context, DotNetCorePackSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.Aero}/{Projects.Aero}.csproj");
            _dotNetCore.Pack(path.FullPath, defaultSettings);
        }

        private void PackAeroAzure(MyContext context, DotNetCorePackSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroAzure}/{Projects.AeroAzure}.csproj");
            _dotNetCore.Pack(path.FullPath, defaultSettings);
        }

        private void PackAeroCake(MyContext context, DotNetCorePackSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroCake}/{Projects.AeroCake}.csproj");
            _dotNetCore.Pack(path.FullPath, defaultSettings);
        }
    }
}
