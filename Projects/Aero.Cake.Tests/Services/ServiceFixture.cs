using Aero.Infrastructure;
using NSubstitute;

namespace Aero.Cake.Services
{
    public class ServiceFixture<T> : TestFixture
    {
        public ServiceFixture()
        {
            Logger = Substitute.For<IAeroLogger<T>>();
        }

        public IAeroLogger<T> Logger;

        public T ServiceUnderTest { get; set; }
    }
}
