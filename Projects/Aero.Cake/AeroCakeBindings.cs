using Aero.Cake.CupCakes;
using Aero.Cake.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aero.Cake
{
    public static class AeroCommonCakeBindings
    {
        public static void Bind(IServiceCollection services)
        {
            //Logging is the decision of the consumer

            services
                .AddSingleton<IDbUpService, DbUpService>()
                .AddSingleton<IDotNetCoreCupCake, DotNetCoreCupCake>()
                .AddSingleton<IVersionService, VersionService>();
        }
    }
}
