using System;
using System.IO;
using System.Linq;
using Aero.Infrastructure;
using FluentAssertions;
using Xunit;

namespace Aero.Common
{
    public class FileSystemTests
    {
        private readonly DirectoryInfo _testDirectory;

        public FileSystemTests()
        {
            var fi = new FileInfo(GetType().Assembly.Location);
            _testDirectory = new DirectoryInfo(Path.Combine(fi.DirectoryName, "FileSystemTests"));

            if (_testDirectory.Exists)
            {
                _testDirectory.Delete(true);
            }

            _testDirectory.Create();
        }

        ~FileSystemTests()
        {
            if (_testDirectory == null)
            {
                return;
            }

            if (_testDirectory.Exists)
            {
                _testDirectory.Delete(true);
            }
        }

        [Fact]
        public void GetSpecialFolder_WithOut_Special_Folder_Specified()
        {
            //Arrange
            var fs = new FileSystem();

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => fs.GetSpecialFolder(SpecialFolder.None));
        }

        [Fact]
        public void GetSpecialFolder_With_Special_Folder_LocalApplicationData_Specified()
        {
            //Arrange
            var fs = new FileSystem();

            //Act
            var s = fs.GetSpecialFolder(SpecialFolder.LocalApplicationData);

            //Assert
            s.Should().Be(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
        }

        [Fact]
        public void GetSpecialFolder_With_Special_Folder_ProgramData_Specified()
        {
            //Arrange
            var fs = new FileSystem();

            //Act
            var s = fs.GetSpecialFolder(SpecialFolder.ProgramData);

            //Assert
            s.Should().Be(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
        }


        [Fact]
        public void GetFullPath_DirectoryExistsIsFalse_Without_Special_Folder_Specified()
        {
            //Arrange
            var fs = new FileSystem();

            //Act
            var d = fs.GetFullPath(@"C:\temp", "Test.txt");

            //Assert
            d.Should().Be(@"C:\temp\Test.txt");
        }

        [Fact]
        public void GetFullPath_DirectoryExistsIsFalse_With_Special_Folder_Specified()
        {
            //Arrange
            var fs = new FileSystem();
            var appDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            //Act
            var d = fs.GetFullPath(@"test", "Test.txt", specialFolder: SpecialFolder.LocalApplicationData);

            //Assert
            d.Should().Be(Path.Combine(appDir, "test", "Test.txt"));
        }

        [Fact]
        public void GetFullPath_DirectoryExistsIsTrue_Create_Directory()
        {

            //Pre-Assert
            _testDirectory.GetDirectories().Count().Should().Be(0);

            //Arrange
            var fs = new FileSystem();

            //Act
            fs.GetFullPath(Path.Combine(_testDirectory.FullName, "NewDirectory"), "Test.txt", true);

            //Assert
            var subDirs = _testDirectory.GetDirectories();

            subDirs.Count().Should().Be(1);
            subDirs.Single(di => di.FullName == Path.Combine(_testDirectory.FullName, "NewDirectory")).Should().NotBeNull();
        }

        [Fact]
        public void CreateDirectory_When_Directory_Does_Not_Exist_Then_Directory_Should_Be_Created()
        {

            //Pre-Assert
            var now = DateTime.UtcNow;
            _testDirectory.GetDirectories().Count().Should().Be(0);

            //Arrange
            var newDirectoryName = Path.Combine(_testDirectory.FullName, "NewDirectory");
            var fs = new FileSystem();

            //Act
            fs.CreateDirectory(newDirectoryName);

            //Assert
            var subDirs = _testDirectory.GetDirectories();
            subDirs.Count().Should().Be(1);
            subDirs.Single(di => di.FullName == newDirectoryName).CreationTimeUtc.Should().BeCloseTo(now,2000);
        }

        [Fact]
        public void CreateDirectory_When_Directory_Exists_Then_Do_Nothing()
        {
            //Pre-Assert
            _testDirectory.GetDirectories().Length.Should().Be(0);

            //Arrange
            var newDirectoryName = Path.Combine(_testDirectory.FullName, "NewDirectory");
            var fs = new FileSystem();
            fs.CreateDirectory(newDirectoryName); //Create directory so it is pre-existing

            //Pre-Assert
            _testDirectory.GetDirectories().Length.Should().Be(1);
            var createdDate = _testDirectory.GetDirectories().Single(x=>x.FullName == newDirectoryName).CreationTimeUtc;

            //Sleep long enough to allow system time to "tick"
            System.Threading.Thread.Sleep(50);

            //Act
            fs.CreateDirectory(newDirectoryName);

            //Assert
            var subDirs = _testDirectory.GetDirectories();
            subDirs.Length.Should().Be(1);
            subDirs.Single(di => di.FullName == newDirectoryName).CreationTimeUtc.Should().Be(createdDate);
        }
    }
}
