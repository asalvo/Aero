namespace Aero.Cake.Services
{
    public abstract class AbstractService
    {
        protected AbstractService(AeroContext aeroContext)
        {
            AeroContext = aeroContext;
        }

        protected AeroContext AeroContext { get; }
    }
}
