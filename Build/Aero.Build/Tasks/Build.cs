using Aero.Cake.CupCakes;
using Aero.Cake.Services;
using Cake.Common;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Frosting;
using Microsoft.Extensions.DependencyInjection;

namespace Aero.Build.Tasks
{
    public class Build : FrostingTask<Context>
    {
        public override void Run(Context context)
        {
            var projectName = context.Argument("ProjectName", "Aero");

            //Set the Version info
            var versionService = context.ServiceProvider.GetService<IVersionSevice>();
            versionService.UpdateFiles(context.Argument<string>("AppVersion"), context.ProjectsPath);

            //Check out the docs on DotNetCore settings. There are some things we want to set, like Configuration, and the replacement for rebuild. 

            var buildSettings = new DotNetCoreBuildSettings
            {
                Configuration = context.Configuration,
                NoIncremental = true
            };

            var dotNetCore = context.ServiceProvider.GetService<IDotNetCoreCupCake>();
            dotNetCore.Build($"{context.ProjectsPath}/${projectName}/{projectName}.csproj", buildSettings);
        }
    }
}
