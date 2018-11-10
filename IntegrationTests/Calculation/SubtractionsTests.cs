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
    public class SubtractionsTests : IntegrationTestBase
    {
        private const int SetsCount = 16;

        [Theory]
        [InlineData(-2, -3, 1)]
        [InlineData(0, 0, 0)]
        [InlineData(10, 1, 9)]
        public async Task Should_return_valid_result(int number1, int number2, int result)
        {
            // arrange
            CalculationSourceGenerator
                .GetSubtractionsSource()
                .Returns(new SubtractionsSource
                {
                    Number1 = number1,
                    Number2 = number2,
                    IsValidResult = true
                });

            var expected = Enumerable.Range(0, SetsCount)
                .Select(_ => new Response<SubtractionsResult>
                {
                    Set = new SubtractionsResult
                    {
                        Numbers = new[] { number1, number2 },
                        IsValidResult = true,
                        Result = result
                    }
                });

            // act
            var httpResponse = await TestServer.Value
                .CreateRequest(SubtractionsBaseUrl)
                .GetAsync();

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var results = await Deserialize<Response<SubtractionsResult>[]>(httpResponse);
            results.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(-2, -3, 1)]
        [InlineData(0, 0, 0)]
        [InlineData(10, 1, 9)]
        public async Task Should_return_invalid_result(int number1, int number2, int result)
        {
            // arrange
            CalculationSourceGenerator
                .GetSubtractionsSource()
                .Returns(new SubtractionsSource
                {
                    Number1 = number1,
                    Number2 = number2,
                    IsValidResult = false
                });

            // act
            var httpResponse = await TestServer.Value
                .CreateRequest(SubtractionsBaseUrl)
                .GetAsync();

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var results = await Deserialize<Response<SubtractionsResult>[]>(httpResponse);
            results.Length.Should().Be(SetsCount);
            foreach (var response in results)
            {
                response.Set.Numbers.Should().BeEquivalentTo(new[] { number1, number2 });
                response.Set.IsValidResult.Should().BeFalse();
                response.Set.Result.Should().NotBe(result);
            };
        }
    }
}
