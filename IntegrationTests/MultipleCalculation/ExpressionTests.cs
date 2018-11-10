using FluentAssertions;
using NSubstitute;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Source;
using Xunit;

namespace IntegrationTests.MultipleCalculation
{
    public class ExpressionTests : IntegrationTestBase
    {
        [Theory]
        [InlineData(
            new[] { 85, 27, 7, 3 },
            new[] { Operation.Subtract, Operation.Add, Operation.Subtract },
            "85 - 27 + 7 - 3",
            62)]
        [InlineData(
            new[] { 9, 5, 16, 7 },
            new[] { Operation.Add, Operation.Add, Operation.Subtract },
            "9 + 5 + 16 - 7",
            23)]
        public async Task Should_return_valid_expression(int[] numbers, Operation[] operations, string expression, int result)
        {
            // arrange
            MultipleCalculationSourceGenerator
                .GetExpressionSource()
                .Returns(new ExpressionSource
                {
                    Numbers = numbers,
                    Operations = operations
                });

            var expected = Enumerable.Range(0, 25)
                .Select(_ => new ExpressionResult
                {
                    Expression = expression,
                    Result = result
                });

            // act
            var httpResponse = await TestServer.Value
                .CreateRequest(ExpressionsBaseUrl)
                .GetAsync();

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var sums = await Deserialize<ExpressionResult[]>(httpResponse);
            sums.Should().BeEquivalentTo(expected);
        }
    }
}
