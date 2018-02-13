using System;
using System.IO;

namespace Aero.Infrastructure
{
    public enum SpecialFolder
    {
        None = 0,
        LocalApplicationData,
        ProgramData
    }

    /// <summary>
    /// Provides an abstraction over the FileSystem which can be implemented for each platform, and mocked out for easy unit testing
    /// </summary>
    public interface IFileSystem
    {
        string Combine(params string[] paths);

        string GetFullPath(string directory, string filenameWithExtention, bool ensureDirectoryExists = false, SpecialFolder specialFolder = SpecialFolder.None);

        string GetSpecialFolder(SpecialFolder specialFolder);
    }

    /// <inheritdoc />
    /// <summary>
    /// A .NetStandard implementation of <see cref="IFileSystem"/>
    /// </summary>
    public class FileSystem : IFileSystem
    {
        public string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public void CreateDirectory(string directoryPath)
        {
            var di = new DirectoryInfo(directoryPath);
            if (di.Exists == false)
            {
                di.Create();
            }
        }

        public string GetFullPath(string directory, string filenameWithExtention, bool ensureDirectoryExists = false, SpecialFolder specialFolder = SpecialFolder.None)
        {

            var directoryPath = specialFolder == SpecialFolder.None ? directory : Combine(GetSpecialFolder(specialFolder), directory);
            var fullPath = Combine(directoryPath, filenameWithExtention);

            if (ensureDirectoryExists)
            {
                CreateDirectory(directoryPath);
            }

            return fullPath;
        }

        public string GetSpecialFolder(SpecialFolder specialFolder)
        {
            switch (specialFolder)
            {
                case SpecialFolder.LocalApplicationData:
                    return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                case SpecialFolder.ProgramData:
                    return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                default:
                    throw new ArgumentOutOfRangeException(nameof(specialFolder), "Special Folder not defined");
            }
        }
    }
}
