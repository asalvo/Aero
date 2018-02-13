using FluentAssertions;
using Xunit;

namespace Aero.Common
{
    public class CombGuidTests
    {
        [Fact]
        public void GenerateCombGuid_Creates_NonEmpty_Guid()
        {
            var guid1 = CombGuid.GenerateComb();

            guid1.Should().NotBeEmpty();

            //for (int i = 0; i < 100; i++)
            //{
            //    System.Diagnostics.Debug.WriteLine(CombGuid.GenerateComb());
            //}
        }
    }
}
