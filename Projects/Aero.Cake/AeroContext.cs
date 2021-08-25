using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;
using System.IO;

namespace Aero.Cake
{
    public interface IAeroContext : IFrostingContext
    {
        IDirectory GetNormalizedDirectory(string relativePath);
        IFile GetNormalizedFile(string relativePath, bool throwIfNotExists = true);
        string GetNormalizedPath(string relativePath);
    }

    public abstract class AeroContext : FrostingContext, IAeroContext
    {
        protected AeroContext(ICakeContext context) : base(context)
        {
        }

        public IFile GetNormalizedFile(string relativePath, bool throwIfNotExists = true)
        {
            var normalizedPath = GetNormalizedPath(relativePath);
            var file = FileSystem.GetFile(normalizedPath);

            if (throwIfNotExists && !file.Exists)
                throw new FileNotFoundException($"GetNormalizedFile failed because {relativePath} does not exist at {file.Path.MakeAbsolute(Environment)}");

            return file;
        }

        public IDirectory GetNormalizedDirectory(string relativePath)
        {
            var normalizedPath = GetNormalizedPath(relativePath);
            var directory = FileSystem.GetDirectory(normalizedPath);

            if (!directory.Exists)
                throw new FileNotFoundException($"{relativePath} does not exist at {directory.Path.MakeAbsolute(Environment)}");

            return directory;
        }

        /// <summary>
        /// Given a path which is relative to the root repository, returns a relative path that is normalized based on runtime conditions
        /// </summary>
        /// <param name="relativePath">The path relative to the root repository</param>
        /// <returns>An adjusted relative path</returns>
        /// <remarks>
        ///   - Depending on how the application is run (debug, command line), and from where (root, root/build/build), the working directory can vary
        /// </remarks>
        /// <example>
        ///    var workDirPath = Environment.WorkingDirectory.FullPath.ToLowerInvariant();
        /// 
        ///    if (workDirPath.EndsWith("build/build/bin/debug"))
        ///         return $"../../../../projects/ProjectName/{relativePath}";
        /// 
        ///     if (workDirPath.EndsWith("build/build"))
        ///         return $"../../projects/ProjectName/{relativePath}";
        /// 
        ///     if (workDirPath.EndsWith("build"))
        ///         return $"../projects/ProjectName/{relativePath}";
        /// 
        ///     return $"RepoRootFolder/projects/ProjectName/{relativePath}";
        /// </example>
        public abstract string GetNormalizedPath(string relativePath);
    }
}
