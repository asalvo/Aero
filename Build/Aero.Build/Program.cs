using Aero.Cake;
using Aero.Cake.Features.DotNet.Services;
using Aero.Cake.Features.DotNet.Wrappers;
using Cake.Frosting;
using Microsoft.Extensions.DependencyInjection;

namespace Aero.Build
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            return new CakeHost()
                .UseContext<MyContext>()
                .UseLifetime<Lifetime>()
                .ConfigureServices(ConfigureServices)
                .Run(args);
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            //Cake - UseContext<MyContext> registers MyContext to IFrostingContext as a singleton
            services.AddSingleton(x => x.GetRequiredService<IFrostingContext>() as IAeroContext);

            //Services

            //Wrappers
            services.AddSingleton<IDotNetCoreWrapper, DotNetCoreWrapper>();
            services.AddSingleton<IVersionService, VersionService>();
        }
    }
}
