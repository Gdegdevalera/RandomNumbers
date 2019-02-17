using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Generators.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultipleCalculationController : ControllerBase
    {
        private readonly IMultipleCalculationSourceGenerator _generator;

        public MultipleCalculationController(IMultipleCalculationSourceGenerator generator)
        {
            _generator = generator;
        }

        [HttpGet]
        [Route("expression")]
        public ExpressionResult[] GetExpression() => 
            Enumerable.Range(0, 25)
                .Select(_ => _generator.GetExpressionSource())
                .Select(x => new
                {
                    Head = x.Numbers.First(),
                    Operands = x.Numbers.Skip(1).Zip(x.Operations, (n, o) => new Operand(n, o))
                })
                .Select(x => new ExpressionResult
                {
                    Expression = $"{x.Head} {string.Join(" ", x.Operands.Select(c => c.ToString()))}",
                    Result = x.Operands.Aggregate(x.Head, (acc, operand) => operand.Apply(acc))
                })
                .ToArray();
    }
}
