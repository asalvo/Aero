namespace Aero.Cake.Wrappers
{
    public abstract class AbstractWrapper
    {
        protected AbstractWrapper(IAeroContext aeroContext)
        {
            AeroContext = aeroContext;
        }

        protected IAeroContext AeroContext { get; }
    }
}