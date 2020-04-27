using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;

namespace Aero.Build
{
    public sealed class Lifetime : FrostingLifetime<MyContext>
    {
        public override void Setup(MyContext context)
        {
            context.Information("Setting things up...");

            //Working Directory changes between when MyContext.ctor is called and when this Lifetime class is run
            context.Information($"WorkingDirectory: {context.Environment.WorkingDirectory.FullPath.ToLowerInvariant()}");
            context.ProjectsPath = context.GetNormalizedPath(string.Empty);
        }

        public override void Teardown(MyContext context, ITeardownContext info)
        {
            context.Information("Tearing things down...");
        }
    }
}