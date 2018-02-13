using System.Collections.Generic;
using Xunit;

namespace Aero.Common
{
    public class RandomNumbersTests
    {
        [Fact]
        public void RandomNumbers_Test()
        {
            //arange
            var list = new List<int> {0, 0, 0, 0, 0}; //init index 0 to 4
            var rn = new RandomNumbers();

            //Act
            for (var i = 0; i < 100; i++)
            {
                list[rn.GetRandomNumber(1, 3)]++;
            }

            Assert.Equal(0, list[0]);
            Assert.True(list[1] > 0);
            Assert.True(list[2] > 0);
            Assert.True(list[3] > 0);
            Assert.Equal(0, list[4]);

        }
    }
}
