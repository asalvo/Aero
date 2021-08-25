namespace Aero.Cake.Services
{
    public abstract class AbstractService
    {
        protected AbstractService(IAeroContext aeroContext)
        {
            AeroContext = aeroContext;
        }

        protected IAeroContext AeroContext { get; }
    }
}
