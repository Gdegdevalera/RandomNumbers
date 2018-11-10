using System.Linq;
using FluentAssertions;
using WebApplication1.Generators;
using Xunit;

namespace Tests
{
    public class GeneratorTests
    {
        [Theory]
        [InlineData(-100, -1)]
        [InlineData(0, 0)]
        [InlineData(0, 9)]
        [InlineData(10, 99)]
        public void Source_should_return_values_within_range(int minValue, int maxValue)
        {
            using (var generator = new RandomNumbersGenerator())
            {
                foreach(var value in generator.Source(minValue, maxValue).Take(1000))
                {
                    value.Should().BeGreaterOrEqualTo(minValue).And.BeLessOrEqualTo(maxValue);
                }
            }
        }

        [Fact]
        public void TrueOrFalse_should_work_like_Head_Or_Tails()
        {
            using (var generator = new RandomNumbersGenerator())
            {
                const int number = 10000;
                var expectation = number / 2;
                uint expectedDelta = number / 50; // delta = 2% 

                foreach (var valueCounter in Enumerable.Range(0, number)
                    .Select(_ => generator.TrueOrFalse())
                    .GroupBy(x => x)
                    .Select(x => x.Count()))
                {
                    valueCounter.Should().BeCloseTo(expectation, expectedDelta);
                }
            }
        }
    }
}
