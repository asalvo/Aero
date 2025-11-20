using Aero.Cake.Wrappers;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.NuGet.Push;
using Cake.Common.Tools.DotNet.NuGet.Source;
using Cake.Common.Tools.DotNet.Pack;
using Cake.Common.Tools.DotNet.Publish;
using Cake.Common.Tools.DotNet.Test;

namespace Aero.Cake.Features.DotNet.Wrappers
{
    /// <summary>
    /// A wrapper class around DotNetCore extensions methods to support unit testing in tasks. Register as a singleton.
    /// </summary>
    public interface IDotNetCoreWrapper
    {
        void Build(string projectPath, DotNetBuildSettings settings);
        void Pack(string projectPath, DotNetPackSettings settings);
        void Publish(string projectPath, DotNetPublishSettings settings);

        void NuGetAddSource(string name, DotNetNuGetSourceSettings settings);
        bool NuGetHasSource(string name, DotNetNuGetSourceSettings settings);
        void NuGetRemoveSource(string name, DotNetNuGetSourceSettings settings);
        void NuGetUpdateSource(string name, DotNetNuGetSourceSettings settings);

        /// <summary>
        /// Uses Dotnet Core to push to Nuget.
        /// </summary>
        /// <remarks>
        /// This works against Nuget.Org but does not work against Azure DevOps without additional configuration. You need to use the Azure Credential Provider
        /// or add an authenticated NuGet source to the NuGet configuration.
        /// </remarks>
        void NuGetPush(string packageName, DotNetNuGetPushSettings settings);

        void Test(string projectPath, DotNetTestSettings settings);
    }

    public class DotNetCoreWrapper : AbstractWrapper, IDotNetCoreWrapper
    {
        public DotNetCoreWrapper(IAeroContext aeroContext) : base(aeroContext)
        {
        }

        public void Build(string projectPath, DotNetBuildSettings settings)
        {
            AeroContext.DotNetBuild(projectPath, settings);
        }

        public void NuGetAddSource(string name, DotNetNuGetSourceSettings settings)
        {
            AeroContext.DotNetNuGetAddSource(name, settings);
        }

        public bool NuGetHasSource(string name, DotNetNuGetSourceSettings settings)
        {
            return AeroContext.DotNetNuGetHasSource(name, settings);
        }

        public void NuGetRemoveSource(string name, DotNetNuGetSourceSettings settings)
        {
            AeroContext.DotNetNuGetRemoveSource(name, settings);
        }

        public void NuGetUpdateSource(string name, DotNetNuGetSourceSettings settings)
        {
            AeroContext.DotNetNuGetUpdateSource(name, settings);
        }

        public void NuGetPush(string packageName, DotNetNuGetPushSettings settings)
        {
            AeroContext.DotNetNuGetPush(packageName, settings);
        }

        public void Pack(string projectPath, DotNetPackSettings settings)
        {
            AeroContext.DotNetPack(projectPath, settings);
        }

        public void Publish(string projectPath, DotNetPublishSettings settings)
        {
            AeroContext.DotNetPublish(projectPath, settings);
        }

        public void Test(string projectPath, DotNetTestSettings settings)
        {
            AeroContext.DotNetTest(projectPath, settings);
        }
    }
}
