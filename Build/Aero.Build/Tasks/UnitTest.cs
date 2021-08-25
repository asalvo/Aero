using Aero.Cake.Features.DotNet.Wrappers;
using Cake.Common.Tools.DotNetCore.Test;
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
            var testSettings = new DotNetCoreTestSettings
            {
                Configuration = context.BuildConfiguration,
                Loggers = new[] {"trx"}
            };

            _dotNetCore.Test($"{context.ProjectsPath}/Aero.Tests/Aero.Tests.csproj", testSettings);
            _dotNetCore.Test($"{context.ProjectsPath}/Aero.Cake.Tests/Aero.Cake.Tests.csproj", testSettings);
        }
    }
}
