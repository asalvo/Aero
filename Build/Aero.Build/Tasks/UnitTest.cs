using Aero.Cake.Services;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Frosting;

namespace Aero.Build.Tasks
{
    public class UnitTest : FrostingTask<MyContext>
    {
        private readonly IDotNetCoreService _dotNetCore;

        public UnitTest(IDotNetCoreService dotNetCore)
        {
            _dotNetCore = dotNetCore;
        }

        public override void Run(MyContext context)
        {
            var testSettings = new DotNetCoreTestSettings
            {
                Configuration = context.BuildConfiguration,
                Logger = "trx"
            };

            _dotNetCore.Test($"{context.ProjectsPath}/Aero.Tests/Aero.Tests.csproj", testSettings);
            _dotNetCore.Test($"{context.ProjectsPath}/Aero.Azure.Tests/Aero.Azure.Tests.csproj", testSettings);
            _dotNetCore.Test($"{context.ProjectsPath}/Aero.Cake.Tests/Aero.Cake.Tests.csproj", testSettings);
        }
    }
}
