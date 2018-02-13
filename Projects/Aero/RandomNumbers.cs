using System;
using System.Security.Cryptography;

namespace Aero
{
    public interface IRandomNumbers
    {
        int GetRandomNumber(int minValue, int maxValue);
    }

    public class RandomNumbers:IRandomNumbers
    {
        private readonly RandomNumberGenerator _random;

        public RandomNumbers()
        {
            _random = new RNGCryptoServiceProvider();
        }

        public int GetRandomNumber(int minValue, int maxValue)
        {
            maxValue = maxValue + 1;

            var b = new byte[4];
            _random.GetNonZeroBytes(b);
            var value = (double)BitConverter.ToUInt32(b, 0) / UInt32.MaxValue;
            return (int)Math.Round(value * (maxValue - minValue - 1)) + minValue;
        }
    }
}
