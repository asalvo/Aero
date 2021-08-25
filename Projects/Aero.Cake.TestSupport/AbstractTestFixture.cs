using System;
using System.IO;
using System.Reflection;

namespace Aero.Cake.TestSupport
{
    /// <summary>
    /// Provides a common base class for Build project unit tests.
    /// </summary>
    /// <remarks>
    /// - This is the only Fixture base class we have. Projects should declare a TestFixture which inherits from this abstract class
    ///   and then create TaskFixture and ServiceFixture which inherit from TestFixture. This allows for project specifics to be added
    ///   in the TestFixture, TaskFixture and ServiceFixture classes.
    /// </remarks>
    public abstract class AbstractTestFixture<TContext> where TContext : AeroContext
    {
        protected AbstractTestFixture()
        {
            MockContext = new MockContext();
        }

        public MockContext MockContext { get; }

        public TContext MyContext { get; protected set; }

        protected abstract Assembly TestAssembly { get; }

        public string TestDirectory
        {
            get
            {
                var path = TestAssembly.CodeBase.Replace("file:///", string.Empty);

                //Do not use Context.Environment here, as it's fake and might be set by the test to OSX when on Windows and vice-versa. 
                if (Environment.OSVersion.Platform == PlatformID.Unix && !path.StartsWith('/'))
                {
                    path = $"/{path}";
                }

                var fi = new FileInfo(path);

                if (!fi.Directory.Exists)
                {
                    throw new DirectoryNotFoundException($"Directory for {path} not found at {fi.Directory.FullName}");
                }

                return fi.Directory.FullName;
            }
        }

        public byte[] ReadFile(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
    }
}