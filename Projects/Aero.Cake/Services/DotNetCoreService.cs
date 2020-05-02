using Cake.Common.Diagnostics;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Common.Tools.DotNetCore.NuGet.Delete;
using Cake.Common.Tools.DotNetCore.NuGet.Push;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Common.Tools.DotNetCore.Publish;
using Cake.Common.Tools.DotNetCore.Test;

namespace Aero.Cake.Services
{
    /// <summary>
    /// A wrapper class around DotNetCore extensions methods to support unit testing in tasks. 
    /// </summary>
    public interface IDotNetCoreService
    {
        void Build(string projectPath, DotNetCoreBuildSettings settings);
        void NuGetDelete(string projectPath, DotNetCoreNuGetDeleteSettings settings);
        void NuGetPush(string projectPath, DotNetCoreNuGetPushSettings settings);
        void Pack(string projectPath, DotNetCorePackSettings settings);
        void Publish(string projectPath, DotNetCorePublishSettings settings);
        void Test(string projectPath, DotNetCoreTestSettings settings);
    }

    public class DotNetCoreService : AbstractService, IDotNetCoreService
    {
        public DotNetCoreService(AeroContext myContext) : base(myContext)
        {

        }

        public void Build(string projectPath, DotNetCoreBuildSettings settings)
        {
            AeroContext.Information($"DotNetCoreService.Buid. Action: Start, ProjectPath: {projectPath}");
            AeroContext.DotNetCoreBuild(projectPath, settings);
        }

        public void NuGetDelete(string projectPath, DotNetCoreNuGetDeleteSettings settings)
        {
            AeroContext.Information($"DotNetCoreService.Delete. Action: Start, ProjectPath: {projectPath}");
            AeroContext.DotNetCoreNuGetDelete(projectPath, settings);
        }

        public void NuGetPush(string projectPath, DotNetCoreNuGetPushSettings settings)
        {
            AeroContext.Information($"DotNetCoreService.NuGetPush. Action: Start, ProjectPath: {projectPath}");
            AeroContext.DotNetCoreNuGetPush(projectPath, settings);
        }

        public void Pack(string projectPath, DotNetCorePackSettings settings)
        {
            AeroContext.Information($"DotNetCoreService.Pack. Action: Start, ProjectPath: {projectPath}");
            AeroContext.DotNetCorePack(projectPath, settings);
        }

        public void Publish(string projectPath, DotNetCorePublishSettings settings)
        {
            AeroContext.Information($"DotNetCoreService.Publish. Action: Start, ProjectPath: {projectPath}");
            AeroContext.DotNetCorePublish(projectPath, settings);
        }

        public void Test(string projectPath, DotNetCoreTestSettings settings)
        {
            AeroContext.Information($"DotNetCoreService.Test. Action: Start, ProjectPath: {projectPath}");
            AeroContext.DotNetCoreTest(projectPath, settings);
        }
    }
}
