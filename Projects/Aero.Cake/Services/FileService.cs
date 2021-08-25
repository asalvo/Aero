using Cake.Common.Diagnostics;

namespace Aero.Cake.Services
{
    public interface IFileService
    {
        byte[] ReadAllBytes(string destinationFullName);

        void ZipDirectory(string sourceDirectoryPath, string destinationFileAndPath, bool deleteExisting = true);
    }

    public class FileService : AbstractService, IFileService
    {
        public FileService(IAeroContext aeroContext) : base(aeroContext)
        {
        }

        public byte[] ReadAllBytes(string destinationFullName)
        {
            return System.IO.File.ReadAllBytes(destinationFullName);
        }

        public void ZipDirectory(string sourceDirectoryPath, string destinationFileAndPath, bool deleteExisting = true)
        {
            AeroContext.Information($"FileService.ZipDirectory. Action: CreateArchive, Source: {sourceDirectoryPath}, Dest: {destinationFileAndPath}");

            var zipFile = new System.IO.FileInfo(destinationFileAndPath);
            if (zipFile.Exists && deleteExisting)
            {
                AeroContext.Information($"FileService.ZipDirectory. Action: DeleteExistingZip, ZipFile: {destinationFileAndPath}");
                zipFile.Delete();
            }

            System.IO.Compression.ZipFile.CreateFromDirectory(sourceDirectoryPath, destinationFileAndPath);
        }
    }
}
