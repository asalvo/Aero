using Aero.Cake.Wrappers;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Common.Tools.DotNetCore.NuGet.Push;
using Cake.Common.Tools.DotNetCore.NuGet.Source;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Common.Tools.DotNetCore.Publish;
using Cake.Common.Tools.DotNetCore.Test;

namespace Aero.Cake.Features.DotNet.Wrappers
{
    /// <summary>
    /// A wrapper class around DotNetCore extensions methods to support unit testing in tasks. Register as a singleton.
    /// </summary>
    public interface IDotNetCoreWrapper
    {
        void Build(string projectPath, DotNetCoreBuildSettings settings);
        void Pack(string projectPath, DotNetCorePackSettings settings);
        void Publish(string projectPath, DotNetCorePublishSettings settings);

        void NuGetAddSource(string name, DotNetCoreNuGetSourceSettings settings);
        bool NuGetHasSource(string name, DotNetCoreNuGetSourceSettings settings);
        void NuGetRemoveSource(string name, DotNetCoreNuGetSourceSettings settings);
        void NuGetUpdateSource(string name, DotNetCoreNuGetSourceSettings settings);

        /// <summary>
        /// Uses Dotnet Core to push to Nuget.
        /// </summary>
        /// <remarks>
        /// This works against Nuget.Org but does not work against Azure DevOps without additional configuration. You need to use the Azure Credential Provider
        /// or add an authenticated NuGet source to the NuGet configuration.
        /// </remarks>
        void NuGetPush(string packageName, DotNetCoreNuGetPushSettings settings);
        
        void Test(string projectPath, DotNetCoreTestSettings settings);
    }

    public class DotNetCoreWrapper : AbstractWrapper, IDotNetCoreWrapper
    {
        public DotNetCoreWrapper(IAeroContext aeroContext) : base(aeroContext)
        {
        }

        public void Build(string projectPath, DotNetCoreBuildSettings settings)
        {
            AeroContext.DotNetCoreBuild(projectPath, settings);
        }

        public void NuGetAddSource(string name, DotNetCoreNuGetSourceSettings settings)
        {
            AeroContext.DotNetCoreNuGetAddSource(name, settings);
        }

        public bool NuGetHasSource(string name, DotNetCoreNuGetSourceSettings settings)
        {
            return AeroContext.DotNetCoreNuGetHasSource(name, settings);
        }

        public void NuGetRemoveSource(string name, DotNetCoreNuGetSourceSettings settings)
        {
            AeroContext.DotNetCoreNuGetRemoveSource(name, settings);
        }

        public void NuGetUpdateSource(string name, DotNetCoreNuGetSourceSettings settings)
        {
            AeroContext.DotNetCoreNuGetUpdateSource(name, settings);
        }

        public void NuGetPush(string packageName, DotNetCoreNuGetPushSettings settings)
        {
            AeroContext.DotNetCoreNuGetPush(packageName, settings);
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
