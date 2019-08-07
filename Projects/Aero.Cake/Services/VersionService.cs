using System;
using System.Linq;
using Aero.Infrastructure;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Xml;
using Cake.Core;
using Cake.Core.IO;

namespace Aero.Cake.Services
{
    [Obsolete("Use DotNetCoreMSBuildSettingsExtensions")]
    public interface IVersionService
    {
        [Obsolete("Use")]
        void UpdateFiles(string version, string projectPath, params string[] excludeProjectsNamed);
    }

    public class VersionService : AbstractService, IVersionService
    {
        public VersionService(ICakeContext cakeContext, IAeroLogger<VersionService> logger) : base(cakeContext, logger)
        {

        }

        /// <summary>
        /// Goes thru CsProj files and if they have a AssemblyVersion attribute, updates it
        /// </summary>
        /// <remarks>Keeping version as a parameter instead of looking for the version argument as this is 
        /// something that could be pulled out into a NuGet package at some point</remarks>
        [Obsolete("Use DotNetCoreMSBuildSettingsExtensions")]
        public void UpdateFiles(string version, string projectPath, params string[] excludeProjectsNamed)
        {
            //We'll keep this around for dealing with Azure Functions as I think that has to be Full Framework
            //var assemblyInfoFiles = context.GetFiles($"{context.ProjectsPath}/**/properties/AssemblyInfo.cs");

            var csprojFiles = CakeContext.GetFiles($"{projectPath}/**/*.csproj");
            var excludeProjectsNamedLower = excludeProjectsNamed.Select(x => x.ToLowerInvariant()).ToArray();

            Logger.Debug($"FilePath: {$"{projectPath}/**/*.csproj"}, CsProjCount: {csprojFiles.Count}, ExcludedCount: {excludeProjectsNamed.Length}");

            foreach(var f in csprojFiles)
            {
                if(!excludeProjectsNamedLower.Contains(f.GetFilenameWithoutExtension().FullPath.ToLowerInvariant()))
                    UpdateCsProjFile(f, version);
            }

        }

        private void UpdateCsProjFile(FilePath filePath, string version)
        {
            Logger.Trace($"Start. Version: {version}, File: {filePath}");

            if (!CakeContext.XmlPeek(filePath, "/Project/@Sdk").StartsWith("Microsoft.NET.Sdk"))
            {
                Logger.Warn("Ignoring csproj, no @Sdk attribute. Version: {version}, File: {filePath}");
                return;
            }

            version = Extensions.DotNetCoreMSBuildSettingsExtensions.ParseVersion(version);

            CakeContext.Information($"Update csproj for version. Version: {version}, File: {filePath}");
            CakeContext.XmlPoke(filePath, "/Project/PropertyGroup/AssemblyVersion", version);
            CakeContext.XmlPoke(filePath, "/Project/PropertyGroup/FileVersion", version);
            CakeContext.XmlPoke(filePath, "/Project/PropertyGroup/Version", version);
        }
    }
}
