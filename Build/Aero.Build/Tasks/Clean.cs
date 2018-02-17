using Cake.Common.IO;
using Cake.Frosting;

namespace Aero.Build.Tasks
{ 
    public class Clean : FrostingTask<Context>
    {
        public override void Run(Context context)
        {
            //Aero.Build references Aero.Cake -> Aero. We'll execute Aero.Build in debug, but build
            //Aero under release, so just clean Release. 

            var directories = context.GetDirectories($"{context.ProjectsPath}/**/bin/Release")
                + context.GetDirectories($"{context.ProjectsPath}/**/obj/Release");

            foreach (var directory in directories)
            {
                context.CleanDirectory(directory);
            }
        }
    }
}
