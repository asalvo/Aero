using Aero.Cake.Services;
using Aero.Cake.Wrappers;
using Cake.Testing;
using FluentAssertions;
using Xunit;

namespace Aero.Cake.Features.DotNet.Services
{
    public class ProjectFileServiceFacts : ServiceFixture<ProjectFileService>
    {
        private readonly string _testFilesDirectory;
        private readonly IXmlWrapper _xmlWrapper;

        public ProjectFileServiceFacts()
        {
            _testFilesDirectory = $"{TestDirectory}/Features/DotNet/Services/TestFiles";
            _xmlWrapper = new XmlWrapper(MyContext);

            ServiceUnderTest = new ProjectFileService(MyContext, _xmlWrapper);
        }

        [Fact]
        public void GetProjectModels_Return_ProjectModel()
        {
            //Arrange - Create a fake file in the Cake context with our desired contents
            var content = ReadFile($"{_testFilesDirectory}/ProjectFileWith1Framework.txt");
            MockContext.FileSystem.CreateFile("Projects/SomeProject/SomeProject.csproj", content);

            //Act
            var models = ServiceUnderTest.GetProjectModels("SomeProject", "Projects", "Release");

            //Assert
            models.Should().BeEquivalentTo(new ProjectModel[]
            {
                new ProjectModel
                {
                    ProjectName = "SomeProject",
                    PublishDirectory = "Projects/SomeProject/bin/Release/netcoreapp3.1/publish",
                    TargetFramework = "netcoreapp3.1",
                    ZipDeployFilename = "Projects/SomeProject/bin/Release/netcoreapp3.1/zipdeploy.zip"
                }
            });
        }

        [Theory]
        [InlineData("ProjectFileWith1Framework.txt", "netcoreapp3.1")] //TargetFramework
        [InlineData("ProjectFileWith2Frameworks.txt", "netstandard2.1;net472")] //TargetFrameworks <-- s at the end
        public void GetTargetFrameworks_Theory(string testFileName, string expectedFrameworks)
        {
            //Arrange - Create a fake file in the Cake context with our desired contents
            var content = ReadFile($"{_testFilesDirectory}/{testFileName}");
            MockContext.FileSystem.CreateFile("project.csproj", content);

            //Act
            var targetFrameworks = ServiceUnderTest.GetTargetFrameworks("project.csproj");

            //Assert
            targetFrameworks.Should().BeEquivalentTo(expectedFrameworks.Split(";"));
        }
    }
}
