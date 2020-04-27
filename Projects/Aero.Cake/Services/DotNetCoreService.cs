using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
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
            AeroContext.DotNetCoreBuild(projectPath, settings);
        }

        public void Pack(string projectPath, DotNetCorePackSettings settings)
        {
            AeroContext.DotNetCorePack(projectPath, settings);
        }

        public void Publish(string projectPath, DotNetCorePublishSettings settings)
        {
            AeroContext.DotNetCorePublish(projectPath, settings);
        }

        public void Test(string projectPath, DotNetCoreTestSettings settings)
        {
            AeroContext.DotNetCoreTest(projectPath, settings);
        }
    }
}
