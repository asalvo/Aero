using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.Frosting;
using Cake.Testing;
using NSubstitute;

namespace Aero.Cake
{
    public class MyContextTester : FrostingContext
    {
        public MyContextTester(ICakeContext context)
           : base(context)
        {

        }

        public string ProjectsPath => $"{RepoRootPath}/projects";

        public string RepoRootPath => "..";

        public IServiceProvider ServiceProvider { get; set; }
    }

    public class CakeContextFixture : ICakeContext
    {
        public CakeContextFixture()
        {
            Arguments = new CakeArguments();
            Environment = FakeEnvironment.CreateUnixEnvironment();
            FileSystem = new FakeFileSystem(Environment);
            Globber = Substitute.For<IGlobber>();
            Log = new FakeLog();
        }

        public ICakeDataResolver Data => throw new NotImplementedException();

        public FakeEnvironment Environment { get; }
        ICakeEnvironment ICakeContext.Environment => Environment;

        public FakeFileSystem FileSystem { get; }
        IFileSystem ICakeContext.FileSystem => FileSystem;

        public IGlobber Globber { get; set; }

        public FakeLog Log { get; }
        ICakeLog ICakeContext.Log => Log;

        public CakeArguments Arguments {get;}
        ICakeArguments ICakeContext.Arguments => Arguments;

        public IProcessRunner ProcessRunner => throw new NotImplementedException();

        public IRegistry Registry => throw new NotImplementedException();

        public IToolLocator Tools => throw new NotImplementedException();
        
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
        
        public string GetArgument(string name)
        {
            return Arguments.ContainsKey(name) ? Arguments[name] : null;
        }
    }
}
