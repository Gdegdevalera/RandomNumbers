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
    public class SearchTests : IntegrationTestBase
    {
        [Theory]
        [MemberData(nameof(GetTestSearchData))]
        public async Task Should_return_search_result(SearchResult expectedResult)
        {
            // arrange
            SeriesSourceGenerator
                .GetSearchSource()
                .Returns(new SearchSource
                {
                    Numbers = expectedResult.NumbersToFind,
                    Array = expectedResult.Array
                });

            // act
            var httpResponse = await TestServer.Value
                .CreateRequest(SearchBaseUrl)
                .GetAsync();

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var sums = await Deserialize<SearchResult>(httpResponse);
            sums.Should().BeEquivalentTo(expectedResult);
        }

        public static IEnumerable<object[]> GetTestSearchData()
        {
            yield return new[] {
                new SearchResult
                {
                    NumbersToFind = new [] { 2, 5, 7, 9 },
                    Array = new[] { 3,4,5,6,4,7,8,9,2,3,5,4,6,7,7,7,4,6,3,7,8,8,1,3,4 },
                    Result = new Dictionary<string, int[]>
                    {
                        { "PositionsOfNumber1", new[] { 8 } },
                        { "PositionsOfNumber2", new[] { 2, 10 } },
                        { "PositionsOfNumber3", new[] { 5, 13, 14, 15, 19 } },
                        { "PositionsOfNumber4", new[] { 7 } },
                    }
                }
            };
        }
    }
}
