using Aero.Cake;
using Aero.Cake.Services;
using Cake.Core.Composition;
using Cake.Frosting;

namespace Aero.Build
{
    class Program : IFrostingStartup
    {
        public static int Main(string[] args)
        {
            // Create the host.
            var host = new CakeHostBuilder()
                .WithArguments(args)
                .UseStartup<Program>()
                .Build();

            // Run the host.
            return host.Run();
        }

        public void Configure(ICakeServices services)
        {
            services.UseWorkingDirectory("..");
            services.UseContext<MyContext>();
            services.UseLifetime<Lifetime>();

            services.RegisterType<MyContext>().As<AeroContext>();

            services.RegisterType<DotNetCoreService>().As<IDotNetCoreService>().Singleton();
        }
    }
}
