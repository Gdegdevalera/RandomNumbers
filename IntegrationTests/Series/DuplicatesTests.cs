using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Source;
using Xunit;

namespace IntegrationTests.Series
{
    public class DuplicatesTests : IntegrationTestBase
    {
        [Theory]
        [MemberData(nameof(GetTestDuplicatesData))]
        public async Task Should_return_duplicates(DuplicatesResult expectedResult)
        {
            // arrange
            SeriesSourceGenerator
                .GetDuplicatesSource()
                .Returns(new DuplicatesSource { Set = expectedResult.Set });

            var expected = Enumerable.Range(0, 20).Select(_ => expectedResult);

            // act
            var httpResponse = await TestServer.Value
                .CreateRequest(DuplicatesBaseUrl)
                .GetAsync();

            // assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var sums = await Deserialize<DuplicatesResult[]>(httpResponse);
            sums.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> GetTestDuplicatesData()
        {
            yield return new[] {
                new DuplicatesResult
                {
                    Set = new[] {
                        new[] { 4, 1, 0, 0 },
                        new[] { 4, 5, 8, 9 },
                        new[] { 9, 6, 2, 5 },
                        new[] { 9, 6, 4, 7 }},
                    ColumnsWithDuplicate = new[] { 0, 1 },
                    RowsWithDuplicate = new[] { 0 }
                }
            };

            yield return new[] {
                new DuplicatesResult
                {
                    Set = new[] {
                        new [] { 1, 2, 3, 4 },
                        new [] { 2, 3, 3, 5 },
                        new [] { 6, 7, 8, 9 },
                        new [] { 0, 8, 2, 7 }},
                    ColumnsWithDuplicate = new[] { 2 },
                    RowsWithDuplicate = new[] { 1 }
                }
            };

            yield return new[] {
                new DuplicatesResult
                {
                    Set = new[] {
                        new [] { 2, 5, 8, 9 },
                        new [] { 5, 8, 9, 6 },
                        new [] { 3, 1, 5, 7 },
                        new [] { 0, 1, 2, 7 }},
                    ColumnsWithDuplicate = new[] { 1, 3 },
                    RowsWithDuplicate = Array.Empty<int>()
                }
            };
        }
    }
}
