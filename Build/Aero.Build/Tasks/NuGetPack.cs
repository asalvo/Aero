using System;
using Aero.Cake.CupCakes;
using Cake.Common;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using Microsoft.Extensions.DependencyInjection;

namespace Aero.Build.Tasks
{
    public class NuGetPack : FrostingTask<Context>
    {
        public override void Run(Context context)
        {
            var appVersion = context.Argument<string>("AppVersion");
            
            var dotNetCore = context.ServiceProvider.GetService<IDotNetCoreCupCake>();

            //https://github.com/NuGet/Home/wiki/Adding-nuget-pack-as-a-msbuild-target
            var settings = new DotNetCorePackSettings
            {
                Configuration = context.Configuration,
                NoBuild = true,
                ArgumentCustomization = args => args
                    .Append($"/p:Version={appVersion}")
                    .Append($"/p:Copyright=\"Copyright {DateTime.UtcNow.Year} Adam Salvo\"")
            };
            
            PackAero(context, dotNetCore, settings);
            PackAeroAzure(context, dotNetCore, settings);
            PackAeroCake(context, dotNetCore, settings);
        }

        private void PackAero(Context context, IDotNetCoreCupCake dotNetCore, DotNetCorePackSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/Aero/Aero.csproj");
            dotNetCore.Pack(path.FullPath, defaultSettings);
        }

        private void PackAeroAzure(Context context, IDotNetCoreCupCake dotNetCore, DotNetCorePackSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/Aero.Azure/Aero.Azure.csproj");
            dotNetCore.Pack(path.FullPath, defaultSettings);
        }

        private void PackAeroCake(Context context, IDotNetCoreCupCake dotNetCore, DotNetCorePackSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/Aero.Cake/Aero.Cake.csproj");
            dotNetCore.Pack(path.FullPath, defaultSettings);
        }
    }
}
