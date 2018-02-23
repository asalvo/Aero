using Aero.Infrastructure;
using Cake.Core;

namespace Aero.Cake.Services
{
    public abstract class AbstractService
    {
        protected AbstractService(ICakeContext cakeContext, IAeroLogger logger)
        {
            CakeContext = cakeContext;
            Logger = logger;
        }

        public ICakeContext CakeContext { get; }

        public IAeroLogger Logger { get; }
    }
}
