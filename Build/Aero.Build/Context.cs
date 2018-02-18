using System;
using Cake.Core;
using Cake.Frosting;

namespace Aero.Build
{
    public class Context : FrostingContext
    {
        public Context(ICakeContext context)
            : base(context)
        {

        }

        public string AppVersion { get; set; }

        public string Configuration { get; set; }

        public string ProjectsPath => $"{RepoRootPath}/projects";

        public IServiceProvider ServiceProvider { get; set; }

        public string RepoRootPath => "..";

        public string SolutionFile { get; set; }

        public string Target { get; set; }
        
    }
}
