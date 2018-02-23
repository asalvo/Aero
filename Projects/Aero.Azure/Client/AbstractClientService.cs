using Aero.Infrastructure;

namespace Aero.Azure.Client
{
    public abstract class AbstractClientService
    {
        protected AbstractClientService(IAeroLogger logger)
        {
            Logger = logger;
        }

        protected IAeroLogger Logger { get; }
    }
}
