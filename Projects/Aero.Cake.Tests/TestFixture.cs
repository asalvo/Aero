using System.Reflection;
using Aero.Build;
using Aero.Cake.TestSupport;

namespace Aero.Cake
{
    public abstract class TestFixture : AbstractTestFixture<MyContext>
    {
        private static readonly Assembly _testAssembly = typeof(TestFixture).Assembly;

        protected TestFixture()
        {
            MyContext = new MyContext(MockContext);
            MyContext.LifetimeInitialized();
        }

        protected override Assembly TestAssembly => _testAssembly;
    }
}
