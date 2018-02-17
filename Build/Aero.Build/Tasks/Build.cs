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
            //Set the Version info
            var versionService = context.ServiceProvider.GetService<IVersionSevice>();
            versionService.UpdateFiles(context.Argument<string>("AppVersion"), context.ProjectsPath, "VersionAttributeDoesNotExist");

            //Check out the docs on DotNetCore settings. There are some things we want to set, like Configuration, and the replacement for rebuild. 

            var buildSettings = new DotNetCoreBuildSettings
            {
                Configuration = context.Configuration,
                NoIncremental = true
            };

            var dotNetCore = context.ServiceProvider.GetService<IDotNetCoreCupCake>();

            //This build project is going to build everything in debug mode. Then we will build in release mode. 
            dotNetCore.Build($"{context.ProjectsPath}/Aero.Azure/Aero.Azure.csproj", buildSettings);
            dotNetCore.Build($"{context.ProjectsPath}/Aero.Cake/Aero.Cake.csproj", buildSettings);
        }
    }
}
