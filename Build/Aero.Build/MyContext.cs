using Aero.Cake;
using Aero.Cake.WellKnown;
using Cake.Common;
using Cake.Core;

namespace Aero.Build
{
    public class MyContext : AeroContext
    {
        public MyContext(ICakeContext context)
            : base(context)
        {
            //Force Release so Aero.Build can reference Aero.Cake and not a NuGet. context.Argument("configuration", "Release");
            BuildConfiguration = context.Argument(ArgumentNames.BuildConfiguration, "Release");
        }

        public void LifetimeInitialized()
        {
            //Working Directory changes between when MyContext.ctor is called and when this Lifetime class is run
            ProjectsPath = GetNormalizedPath(string.Empty);
        }

        public string BuildConfiguration { get; set; }

        public string ProjectsPath { get; set; }

        public override string GetNormalizedPath(string relativePath)
        {
            var workDirPath = Environment.WorkingDirectory.FullPath.ToLowerInvariant();

            if (workDirPath.EndsWith("build/aero.build/bin/debug"))
                return System.IO.Path.Combine("../../../../projects", relativePath);

            if (workDirPath.EndsWith("build/aero.build"))
                return System.IO.Path.Combine("../../projects", relativePath);

            if (workDirPath.EndsWith("build"))
                return System.IO.Path.Combine("../projects", relativePath);

            return System.IO.Path.Combine("Aero/projects", relativePath);
        }
    }
}
