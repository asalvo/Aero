using Aero.Build.WellKnown;
using Aero.Cake.Features.DotNet.Settings;
using Aero.Cake.Features.DotNet.Wrappers;
using Cake.Frosting;

namespace Aero.Build.Tasks
{
    public class UnitTest : FrostingTask<MyContext>
    {
        private readonly IDotNetCoreWrapper _dotNetCore;

        public UnitTest(IDotNetCoreWrapper dotNetCore)
        {
            _dotNetCore = dotNetCore;
        }

        public override void Run(MyContext context)
        {
            //This task assumes that the test projects and all dependent projects were built in the Build Task.
            //So we set NoBuild to true

            var testSettings = TestSettings.Default().SetNoBuildNoRestore(true);

            _dotNetCore.Test($"{context.ProjectsPath}/{Projects.Aero}.Tests/{Projects.Aero}.Tests.csproj", testSettings);
            _dotNetCore.Test($"{context.ProjectsPath}/{Projects.AeroCake}.Tests/{Projects.AeroCake}.Tests.csproj", testSettings);
        }
    }
}
