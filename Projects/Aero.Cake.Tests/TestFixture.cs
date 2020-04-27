using System;
using System.IO;
using System.Reflection;

namespace Aero.Cake
{
    public abstract class TestFixture
    {
        private static readonly Assembly TestAssembly = typeof(TestFixture).Assembly;

        protected TestFixture()
        {
            MockContext = new CakeContextMock();
            MyContext = new MyContext(MockContext);
        }

        public CakeContextMock MockContext { get; }

        public MyContext MyContext { get; }

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
