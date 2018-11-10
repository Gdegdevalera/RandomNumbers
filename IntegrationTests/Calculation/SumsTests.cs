using FluentAssertions;
using NSubstitute;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Source;
using Xunit;

namespace IntegrationTests.Calculation
{
    public class SumsTests : IntegrationTestBase
    {
        private const int SetsCount = 16;

        [Theory]
        [InlineData(new[] { -2, -3 }, -5)]
        [InlineData(new[] { 0, 0 }, 0)]
        [InlineData(new[] { 1, 10 }, 11)]
        [InlineData(new[] { -1, 100, 10 }, 109)]
        public async Task Should_return_valid_result(int[] numbers, int result)
        {
            // arrange
            CalculationSourceGenerator
                .GetSumSource()
                .Returns(new SumsSource
                {
                    Numbers = numbers,
                    IsValidResult = true
                });

            var expected = Enumerable.Range(0, 16)
                .Select(_ => new Response<SumsResult>
                {
                    Set = new SumsResult
                    {
                        Numbers = numbers,
                        IsValidResult = true,
                        Result = result
                    }
                });

            // act
            var httpResponse = await TestServer.Value
                .CreateRequest(SumsBaseUrl)
                .GetAsync();

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var sums = await Deserialize<Response<SumsResult>[]>(httpResponse);
            sums.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(new[] { -2, -3 }, -5)]
        [InlineData(new[] { 0, 0 }, 0)]
        [InlineData(new[] { 1, 10 }, 11)]
        [InlineData(new[] { -1, 100, 10 }, 109)]
        public async Task Should_return_invalid_result(int[] numbers, int result)
        {
            // arrange
            CalculationSourceGenerator
                .GetSumSource()
                .Returns(new SumsSource
                {
                    Numbers = numbers,
                    IsValidResult = false
                });

            // act
            var httpResponse = await TestServer.Value
                .CreateRequest(SumsBaseUrl)
                .GetAsync();

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var results = await Deserialize<Response<SumsResult>[]>(httpResponse);
            results.Length.Should().Be(16);
            foreach (var response in results)
            {
                response.Set.Numbers.Should().BeEquivalentTo(numbers);
                response.Set.IsValidResult.Should().BeFalse();
                response.Set.Result.Should().NotBe(result);
            };
        }
    }
}
