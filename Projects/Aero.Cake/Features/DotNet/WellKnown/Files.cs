namespace Aero.Cake.Features.DotNet.WellKnown
{
    /// <summary>
    /// Well known paths and file names for .Net Core. .Net Framework paths should not be added and should be added to the project/repository specific build project.
    /// </summary>
    public static class Files
    {
        public static string ProjectFile(string projectsPath, string projectName) => $"{projectsPath}/{projectName}/{projectName}.csproj";
    }
}
