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
    public class DivisionsTests : IntegrationTestBase
    {
        [Theory]
        [InlineData(100, 2, 50, true)]
        [InlineData(9999, 9, 1111, true)]
        [InlineData(200, 3, 66, false)]
        public async Task Should_return_divisions(int number1, int number2, int result, bool isValidResult)
        {
            // arrange
            CalculationSourceGenerator
                .GetDivisionsSource()
                .Returns(new DivisionsSource
                {
                    Number1 = number1,
                    Number2 = number2,
                });

            var expected = Enumerable.Range(0, 16)
                .Select(_ => new Response<DivisionsResult>
                {
                    Set = new DivisionsResult
                    {
                        Number1 = number1,
                        Number2 = number2,
                        IsValidResult = isValidResult,
                        Result = result
                    }
                });

            // act
            var httpResponse = await TestServer.Value
                .CreateRequest(DivisionsBaseUrl)
                .GetAsync();

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var results = await Deserialize<Response<DivisionsResult>[]>(httpResponse);
            results.Should().BeEquivalentTo(expected);
        }
    }
}
