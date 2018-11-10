using FluentAssertions;
using WebApplication1.Extensions;
using Xunit;

namespace Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void Transpose_should_work_properly()
        {
            var input = new[] 
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5, 6 },
                new[] { 7, 8, 9 },
                new[] { 10, 11, 12 },
            };

            var expected = new[] 
            {
                new[] { 1, 4, 7, 10 },
                new[] { 2, 5, 8, 11 },
                new[] { 3, 6, 9, 12 }
            };

            var actual = input.Transpose();

            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(new[] { 1, 2 }, new[] { 7 })]
        [InlineData(new[] { 5, 9 }, new[] { 0, 14 })]
        public void Array_paris_search_should_work_properly(int[] pair, int[] expected)
        {
            var array = new[] { 5, 9, 3, 2, 1, 5, 8, 1, 2, 5, 5, 7, 8, 9, 5, 9, 3, 0, 8, 6, 5, 4, 6, 7, 8 };

            var actual = array.Search(pair);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
