using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;

namespace Aero.Build
{
    public sealed class Lifetime : FrostingLifetime<MyContext>
    {
        public override void Setup(MyContext context, ISetupContext info)
        {
            context.Information($"Lifetime.Setup. Action: Start, TargetTask: {info.TargetTask}");

            context.LifetimeInitialized();

            context.Information("Lifetime.Setup. Action: Stop");
        }

        public override void Teardown(MyContext context, ITeardownContext info)
        {
            context.Information("Lifetime.Teardown.  Action: Stop");
        }
    }
}