using Aero.Build.WellKnown;
using Aero.Cake.Features.DotNet.Services;
using Aero.Cake.Features.DotNet.Settings;
using Aero.Cake.Features.DotNet.Wrappers;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Core.IO;
using Cake.Frosting;

namespace Aero.Build.Tasks
{
    public class NuGetPack : FrostingTask<MyContext>
    {
        private readonly IDotNetCoreWrapper _dotNetCore;
        private readonly IVersionService _versionService;

        public NuGetPack(IDotNetCoreWrapper dotNetCore, IVersionService versionService)
        {
            _dotNetCore = dotNetCore;
            _versionService = versionService;
        }

        public override void Run(MyContext context)
        {
            var versionModel = _versionService.ParseAppVersion();

            var settings = PackSettings.Default(versionModel, "Adam Salvo");

            PackAero(context, settings);
            PackAeroCake(context, settings);
            PackAeroCakeTestSupport(context, settings);
        }

        private void PackAero(MyContext context, DotNetCorePackSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.Aero}/{Projects.Aero}.csproj");
            _dotNetCore.Pack(path.FullPath, defaultSettings);
        }

        private void PackAeroCake(MyContext context, DotNetCorePackSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroCake}/{Projects.AeroCake}.csproj");
            _dotNetCore.Pack(path.FullPath, defaultSettings);
        }

        private void PackAeroCakeTestSupport(MyContext context, DotNetCorePackSettings defaultSettings)
        {
            var path = new FilePath($"{context.ProjectsPath}/{Projects.AeroCakeTestSupport}/{Projects.AeroCakeTestSupport}.csproj");
            _dotNetCore.Pack(path.FullPath, defaultSettings);
        }
    }
}
