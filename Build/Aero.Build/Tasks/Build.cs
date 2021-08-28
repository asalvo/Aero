﻿using Aero.Build.WellKnown;
using Aero.Cake.Extensions;
using Aero.Cake.Features.DotNet.Wrappers;
using Aero.Cake.WellKnown;
using Cake.Common;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Common.Tools.DotNetCore.MSBuild;
using Cake.Frosting;

namespace Aero.Build.Tasks
{
    public class Build : FrostingTask<MyContext>
    {
        private readonly IDotNetCoreWrapper _dotNetCore;

        public Build(IDotNetCoreWrapper dotNetCoreService)
        {
            _dotNetCore = dotNetCoreService;
        }

        public override void Run(MyContext context)
        {
            var version = context.Argument<string>(ArgumentNames.AppVersion);

            var buildSettings = new DotNetCoreBuildSettings
            {
                Configuration = context.BuildConfiguration,
                NoIncremental = true,
                MSBuildSettings = new DotNetCoreMSBuildSettings()
            };

            buildSettings.MSBuildSettings.SetAllVersions(version);

            //This build project is going to build everything in debug mode. Then we will build in release mode. 
            _dotNetCore.Build($"{context.ProjectsPath}/{Projects.Aero}/{Projects.Aero}.csproj", buildSettings);
            _dotNetCore.Build($"{context.ProjectsPath}/{Projects.AeroCake}/{Projects.AeroCake}.csproj", buildSettings);
            _dotNetCore.Build($"{context.ProjectsPath}/{Projects.AeroCakeTestSupport}/{Projects.AeroCakeTestSupport}.csproj", buildSettings);
        }
    }
}
