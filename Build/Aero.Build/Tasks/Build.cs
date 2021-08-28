using Aero.Build.WellKnown;
using Aero.Cake.Features.DotNet.Services;
using Aero.Cake.Features.DotNet.Settings;
using Aero.Cake.Features.DotNet.Wrappers;
using Cake.Frosting;

namespace Aero.Build.Tasks
{
    /// <summary>
    /// Builds the project by packaging it into a NuPkg. This way when the unit test project runs and potentially rebuilds
    /// all of the projects we don't loose the version info.
    /// </summary>
    /// <remarks>
    /// We could build every project explicitly and then do tests, followed by pack, but I prefer to pack/publish as part of
    /// the build step to catch problems early and it usually doesn't add much overhead.
    /// </remarks>
    public class Build : FrostingTask<MyContext>
    {
        private readonly IDotNetCoreWrapper _dotNetCore;
        private readonly IVersionService _versionService;

        public Build(IDotNetCoreWrapper dotNetCore, IVersionService versionService)
        {
            _dotNetCore = dotNetCore;
            _versionService = versionService;
        }

        public override void Run(MyContext context)
        {
            //- If you build during the test, the dependent projects will be rebuilt and you will loose version info
            //- Options: 
            //  - Run test step first (NoBuild = false), followed by build (pack) for non-test projects with NoBuild set to false
            //  - Run build (pack) first (NoBuild = false) for non-test projects, followed by test step (NoBuild = false)
            //  - Build every project first (pack or build), then run test (NoBuild = true) and optionally pack (NoBuild = true) 

            var versionModel = _versionService.ParseAppVersion();

            var buildSettings = BuildSettings.Default(versionModel);
            var packSettings = PackSettings.Default(versionModel, "Adam Salvo");

            _dotNetCore.Pack($"{context.ProjectsPath}/{Projects.Aero}/{Projects.Aero}.csproj", packSettings);
            _dotNetCore.Pack($"{context.ProjectsPath}/{Projects.AeroCake}/{Projects.AeroCake}.csproj", packSettings);
            _dotNetCore.Pack($"{context.ProjectsPath}/{Projects.AeroCakeTestSupport}/{Projects.AeroCakeTestSupport}.csproj", packSettings);

            _dotNetCore.Build($"{context.ProjectsPath}/{Projects.Aero}.Tests/{Projects.Aero}.Tests.csproj", buildSettings);
            _dotNetCore.Build($"{context.ProjectsPath}/{Projects.AeroCake}.Tests/{Projects.AeroCake}.Tests.csproj", buildSettings);
        }
    }
}
