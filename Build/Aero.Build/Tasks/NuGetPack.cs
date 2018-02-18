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
            var projectToPack = context.Argument<string>("Project");

            var dotNetCore = context.ServiceProvider.GetService<IDotNetCoreCupCake>();

            var path = new FilePath($"{context.ProjectsPath}/{projectToPack}/{projectToPack}.csproj");
            dotNetCore.Pack(path.FullPath, new DotNetCorePackSettings
            {
                Configuration = context.Configuration,
                IncludeSymbols = true,
                NoBuild = true,
                ArgumentCustomization = args => args
                    .Append($"/p:Version={appVersion}")
            });
        }
    }
}
