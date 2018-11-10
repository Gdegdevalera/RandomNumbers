using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Extensions;
using WebApplication1.Generators;
using WebApplication1.Generators.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly IRandomNumbersGenerator _generator;
        private readonly ISeriesSourceGenerator _sourceGenerator;

        public SeriesController(
            IRandomNumbersGenerator generator,
            ISeriesSourceGenerator sourceGenerator)
        {
            _generator = generator;
            _sourceGenerator = sourceGenerator;
        }

        [HttpGet]
        [Route("duplicates")]
        public DuplicatesResult[] GetDuplicates()
        {
            return Enumerable.Range(0, 20)
                .Select(_ => _sourceGenerator.GetDuplicatesSource())
                .Select(x => new DuplicatesResult
                {
                    Set = x.Set,
                    ColumnsWithDuplicate = x.Set.GetColumnWithDuplicate(),
                    RowsWithDuplicate = x.Set.GetRowWithDuplicate()
                })
                .ToArray();
        }

        [HttpGet]
        [Route("search")]
        public SearchResult GetSearch()
        {
            var source = _sourceGenerator.GetSearchSource();
            return new SearchResult
            {
                NumbersToFind = source.Numbers,
                Array = source.Array,
                Result = source.Numbers
                    .Select((x, i) => new { Key = i + 1, Value = source.Array.Search(x) })
                    .ToDictionary(x => $"PositionsOfNumber{x.Key}", x => x.Value)
            };
        }

        [HttpGet]
        [Route("exactsearch")]
        public ExactSearchResult GetExactSearch()
        {
            var source = _sourceGenerator.GetExactSearchSource();
            return new ExactSearchResult
            {
                ExactPairsToFind = source.Pairs,
                Array = source.Array,
                Result = source.Pairs
                    .Select((x, i) => new { Key = i + 1, Value = source.Array.Search(x) })
                    .ToDictionary(x => $"PositionsOfPair{x.Key}", x => x.Value)
            };
        }
    }
}
