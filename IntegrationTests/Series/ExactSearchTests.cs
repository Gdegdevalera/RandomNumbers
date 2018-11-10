using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Source;
using Xunit;

namespace IntegrationTests.Series
{
    public class ExactSearchTests : IntegrationTestBase
    {
        [Theory]
        [MemberData(nameof(GetTestExactSearchData))]
        public async Task Should_return_exact_search_result(ExactSearchResult expectedResult)
        {
            // arrange
            SeriesSourceGenerator
                .GetExactSearchSource()
                .Returns(new ExactSearchSource
                {
                    Pairs = expectedResult.ExactPairsToFind,
                    Array = expectedResult.Array
                });

            // act
            var httpResponse = await TestServer.Value
                .CreateRequest(ExactSearchBaseUrl)
                .GetAsync();

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var sums = await Deserialize<ExactSearchResult>(httpResponse);
            sums.Should().BeEquivalentTo(expectedResult);
        }

        public static IEnumerable<object[]> GetTestExactSearchData()
        {
            yield return new[] {
                new ExactSearchResult
                {
                    ExactPairsToFind = new[] {
                        new[] { 1, 2 },
                        new[] { 5, 9 }
                    },
                    Array = new[] { 5,9,3,2,1,2,8,1,2,5,5,7,8,9,0,1,2,0,8,6,5,9,6,7,8 },
                    Result = new Dictionary<string, int[]>
                    {
                        { "PositionsOfPair1", new[] { 4, 7, 15 } },
                        { "PositionsOfPair2", new[] { 0, 20 } },
                    }
                }
            };
        }
    }
}
