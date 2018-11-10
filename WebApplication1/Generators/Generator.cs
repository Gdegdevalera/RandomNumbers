using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace WebApplication1.Generators
{
    public interface IRandomNumbersGenerator
    {
        IEnumerable<int> Source(int minValue, int maxValue);

        int Next(int minValue, int maxValue);

        bool TrueOrFalse();
    }

    // Based on Scott Lilly implementation https://scottlilly.com/create-better-random-numbers-in-c/
    public class RandomNumbersGenerator : IRandomNumbersGenerator, IDisposable
    {
        private readonly RNGCryptoServiceProvider _crypto = new RNGCryptoServiceProvider();

        public IEnumerable<int> Source(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentException("MinValue must not be greater than MaxValue");

            while (true)
            {
                yield return Next(minValue, maxValue);
            }
        }

        public int Next(int minValue, int maxValue)
        {
            var randomNumber = new byte[1];

            _crypto.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            // We are using Math.Max, and substracting 0.00000000001, 
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = maxValue - minValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minValue + randomValueInRange);
        }

        public bool TrueOrFalse()
        {
            byte[] randomNumber = new byte[1];

            _crypto.GetBytes(randomNumber);

            return randomNumber[0] >= byte.MaxValue / 2;
        }

        public void Dispose()
        {
            _crypto?.Dispose();
        }
    }
}
