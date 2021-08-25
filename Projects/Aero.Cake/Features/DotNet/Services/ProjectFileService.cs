using System.Collections.Generic;
using Aero.Cake.Features.DotNet.WellKnown;
using Aero.Cake.Services;
using Aero.Cake.Wrappers;
using Cake.Core.IO;

namespace Aero.Cake.Features.DotNet.Services
{
    public interface IProjectFileService
    {
        /// <summary>
        /// Given a projects path and project name, returns a list of ProjectModels which contains information about the project used for building and deploying
        /// </summary>
        ProjectModel[] GetProjectModels(string projectName, string projectsPath, string buildConfiguration);

        string[] GetTargetFrameworks(string projectPath);
    }

    public class ProjectFileService : AbstractService, IProjectFileService
    {
        private readonly IXmlWrapper _xmlWrapper;

        public ProjectFileService(IAeroContext myContext, IXmlWrapper xmlWrapper) : base(myContext)
        {
            _xmlWrapper = xmlWrapper;
        }

        public ProjectModel[] GetProjectModels(string projectName, string projectsPath, string buildConfiguration)
        {
            var projectPath = Files.ProjectFile(projectsPath, projectName);

            var targetFrameworks = GetTargetFrameworks(projectPath);

            var projects = new List<ProjectModel>();

            foreach (var tf in targetFrameworks)
            {
                projects.Add(new ProjectModel
                {
                    ProjectName = projectName,
                    PublishDirectory = $"{projectsPath}/{projectName}/bin/{buildConfiguration}/{tf}/publish",
                    TargetFramework = tf,
                    ZipDeployFilename = $"{projectsPath}/{projectName}/bin/{buildConfiguration}/{tf}/zipdeploy.zip"
                });
            }

            return projects.ToArray();
        }

        public string[] GetTargetFrameworks(string projectPath)
        {
            //Check for TargetFramework first, if it exists, TargetFrameworks will not exist
            var targetFrameworkString = _xmlWrapper.XmlPeek(new FilePath(projectPath), "/Project/PropertyGroup/TargetFramework");

            if(!string.IsNullOrWhiteSpace(targetFrameworkString))
            {
                return new string[] { targetFrameworkString };
            }

            targetFrameworkString = _xmlWrapper.XmlPeek(new FilePath(projectPath), "/Project/PropertyGroup/TargetFrameworks");

            return targetFrameworkString.Split(";");
        }
    }

    public class ProjectModel
    {
        public string ProjectName { get; set; }

        public string PublishDirectory { get; set; }

        public string TargetFramework { get; set; }

        public string ZipDeployFilename { get; set; }
    }
}
