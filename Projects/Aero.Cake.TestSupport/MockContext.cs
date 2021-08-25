using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Configuration;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.Testing;
using NSubstitute;

namespace Aero.Cake.TestSupport
{
    public class MockContext : ICakeContext
    {
        public MockContext()
        {
            Arguments = new CakeArguments();
            Environment = FakeEnvironment.CreateUnixEnvironment();
            FileSystem = new FakeFileSystem(Environment);
            Globber = Substitute.For<IGlobber>();
            Log = new FakeLog();

            var configuration = new FakeConfiguration();
            Configuration = configuration;
            Registry = new WindowsRegistry();
            Tools = new ToolLocator(Environment, new ToolRepository(Environment), new ToolResolutionStrategy(FileSystem, Environment, Globber, configuration, Log));
            ProcessRunner = new ProcessRunner(FileSystem, Environment, Log, Tools, configuration);
        }

        public CakeArguments Arguments { get; }
        ICakeArguments ICakeContext.Arguments => Arguments;

        public FakeEnvironment Environment { get; set; }
        ICakeEnvironment ICakeContext.Environment => Environment;

        public ICakeDataResolver Data => throw new NotImplementedException("Not Implemented in CakeContextMock");
        public ICakeConfiguration Configuration { get; }

        public FakeFileSystem FileSystem { get; }
        IFileSystem ICakeContext.FileSystem => FileSystem;

        public IGlobber Globber { get; set; }

        public FakeLog Log { get; }
        ICakeLog ICakeContext.Log => Log;

        public IProcessRunner ProcessRunner { get; }

        public IRegistry Registry { get; }

        public IToolLocator Tools { get; }
    }

    public class CakeArguments : ICakeArguments
    {
        public CakeArguments()
        {
            Arguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        private IDictionary<string, string> Arguments { get; }

        public void AddArgument(string key, string value) { Arguments.Add(key, value); }

        public bool HasArgument(string name)
        {
            return Arguments.ContainsKey(name);
        }

        public ICollection<string> GetArguments(string name)
        {
            return Arguments.TryGetValue(name, out var arguments) ? new[] { arguments } : Array.Empty<string>();
        }
    }
}