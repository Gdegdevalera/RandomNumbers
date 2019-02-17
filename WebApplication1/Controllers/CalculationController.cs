using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Generators.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly ICalculationSourceGenerator _sourceGenerator;

        public CalculationController(ICalculationSourceGenerator sourceGenerator)
        {
            _sourceGenerator = sourceGenerator;
        }

        [HttpGet]
        [Route("sums")]
        public Response<SumsResult>[] GetSums() =>
            Enumerable.Range(0, 16)
                .Select(_ => _sourceGenerator.GetSumSource())
                .Select(x => new SumsResult
                {
                    Numbers = x.Numbers,
                    Result = x.IsValidResult
                        ? x.Numbers.Sum()       // make correct result
                        : x.Numbers.Sum() + 1,  // make wrong result
                    IsValidResult = x.IsValidResult
                })
                .Select(set => new Response<SumsResult> { Set = set })
                .ToArray();

        [HttpGet]
        [Route("subtractions")]
        public Response<SubtractionsResult>[] GetSubtractions() =>
            Enumerable.Range(0, 16)
                .Select(_ => _sourceGenerator.GetSubtractionsSource())
                .Select(x => new SubtractionsResult
                {
                    Numbers = new[] { x.Number1, x.Number2 },
                    Result = x.IsValidResult
                        ? x.Number1 - x.Number2        // make correct result
                        : x.Number1 - x.Number2 + 1,   // make wrong result
                    IsValidResult = x.IsValidResult
                })
                .Select(set => new Response<SubtractionsResult> { Set = set })
                .ToArray();

        [HttpGet]
        [Route("multiplications")]
        public Response<MultiplicationsResult>[] GetMultiplications() =>
            Enumerable.Range(0, 16)
                .Select(_ => _sourceGenerator.GetMultiplicationsSource())
                .Select(x => new MultiplicationsResult
                {
                    Number1 = x.Number1,
                    Number2 = x.Number2,
                    Result = x.IsValidResult
                        ? x.Number1 * x.Number2         // make correct result
                        : x.Number1 * x.Number2 + 1,    // make wrong result
                    IsValidResult = x.IsValidResult
                })
                .Select(set => new Response<MultiplicationsResult> { Set = set })
                .ToArray();

        [HttpGet]
        [Route("divisions")]
        public Response<DivisionsResult>[] GetDivisions() =>
            Enumerable.Range(0, 16)
                .Select(_ => _sourceGenerator.GetDivisionsSource())
                .Select(x => new DivisionsResult
                {
                    Number1 = x.Number1,
                    Number2 = x.Number2,
                    Result = x.Number1 / x.Number2,
                    IsValidResult = x.Number1 % x.Number2 == 0
                })
                .Select(set => new Response<DivisionsResult> { Set = set })
                .ToArray();
    }
}
