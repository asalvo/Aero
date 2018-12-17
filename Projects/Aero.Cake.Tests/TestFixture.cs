using System;
using System.IO;
using System.Reflection;
using NSubstitute;

namespace Aero.Cake
{
    public abstract class TestFixture
    {
        private static readonly Assembly TestAssembly = typeof(TestFixture).Assembly;

        protected TestFixture()
        {
            CakeContext = new CakeContextFixture();
            ServiceProvider = Substitute.For<IServiceProvider>();

            MyContext = new MyContextTester(CakeContext)
            {
                
            };
        }

        public CakeContextFixture CakeContext { get; }

        public MyContextTester MyContext { get; }

        public IServiceProvider ServiceProvider { get; }

        public T AddService<T>() where T : class
        {
            var sub = Substitute.For<T>();
            ServiceProvider.GetService(typeof(T)).Returns(sub);
            return sub;
        }

        public string TestDirectory
        {
            get
            {
                var fi = new FileInfo(TestAssembly.CodeBase.Replace("file:///", string.Empty));
                return fi.Directory.FullName;
            }
        }

        public Byte[] ReadFile(string filePath)
        {
            var fi = new FileInfo(filePath);
            var bytes = new byte[fi.Length];
            using(var reader = fi.OpenRead())
            {
                reader.Read(bytes, 0, (int)fi.Length);
            }
            return bytes;
        }
    }

}
