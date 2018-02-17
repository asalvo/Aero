using Aero.Cake;
using Aero.Cake.CupCakes;
using Aero.Cake.Services;
using Cake.Common;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Frosting;
using Microsoft.Extensions.DependencyInjection;

namespace Aero.Build
{
    public sealed class Lifetime : FrostingLifetime<Context>
    {
        public override void Setup(Context context)
        {
            context.Information("Setting things up...");

            context.Configuration = "Release"; //Force Release so Aero.Build can reference Aero.Cake and not a NuGet. context.Argument("configuration", "Release");
            context.Target = context.Argument("target", "Default");
            context.ServiceProvider = BuildServiceProvider(context);
            context.SolutionFile = $"{context.RepoRootPath}/{context.Argument("solutionFile", "Aero.sln")}";
            
        }

        public override void Teardown(Context context, ITeardownContext info)
        {
            context.Information("Tearing things down...");
        }

        static ServiceProvider BuildServiceProvider(ICakeContext context)
        {
            //Most azure services are Transient as AzureRmService calls init on each once we have authenticated and know the environment

            var services = new ServiceCollection()
                .AddSingleton(_ => context)
                .AddSingleton<IDotNetCoreCupCake, DotNetCoreCupCake>()
                .AddSingleton(typeof(IAeroLogger<>), typeof(AeroCakeLogger<>))
                .AddSingleton<IVersionSevice, VersionService>();

            return services.BuildServiceProvider();
        }
    }
}