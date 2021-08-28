using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;

namespace Aero.Build
{
    public sealed class Lifetime : FrostingLifetime<MyContext>
    {
        public override void Setup(MyContext context)
        {
            context.Information("Setting things up...");
            context.LifetimeInitialized();
        }

        public override void Teardown(MyContext context, ITeardownContext info)
        {
            context.Information("Tearing things down...");
        }
    }
}