using Aero.Infrastructure;
using NSubstitute;

namespace Aero.Cake.Services
{
    public class ServiceFixture<T> : TestFixture
    {
        public ServiceFixture()
        {

        }

        public T ServiceUnderTest { get; set; }
    }
}
