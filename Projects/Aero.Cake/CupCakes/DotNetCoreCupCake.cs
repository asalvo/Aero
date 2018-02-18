using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Common.Tools.DotNetCore.Publish;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Core;

namespace Aero.Cake.CupCakes
{
    public interface IDotNetCoreCupCake
    {
        void Build(string projectPath, DotNetCoreBuildSettings settings);
        void Pack(string projectPath, DotNetCorePackSettings settings);
        void Publish(string projectPath, DotNetCorePublishSettings settings);
        void Test(string projectPath, DotNetCoreTestSettings settings);
    }

    public class DotNetCoreCupCake : IDotNetCoreCupCake
    {
        private readonly ICakeContext _context;

        public DotNetCoreCupCake(ICakeContext context)
        {
            _context = context;
        }

        public void Build(string projectPath, DotNetCoreBuildSettings settings)
        {
            _context.DotNetCoreBuild(projectPath, settings);
        }

        public void Pack(string projectPath, DotNetCorePackSettings settings)
        {
            _context.DotNetCorePack(projectPath, settings);
        }

        public void Publish(string projectPath, DotNetCorePublishSettings settings)
        {
            _context.DotNetCorePublish(projectPath, settings);
        }

        public void Test(string projectPath, DotNetCoreTestSettings settings)
        {
            _context.DotNetCoreTest(projectPath, settings);
        }
    }
}
