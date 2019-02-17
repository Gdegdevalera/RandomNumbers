using System.Linq;
using WebApplication1.Generators.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.Source;

namespace WebApplication1.Generators
{
    public class SourceGenerator :
        ICalculationSourceGenerator,
        IMultipleCalculationSourceGenerator,
        ISeriesSourceGenerator
    {
        private readonly IRandomNumbersGenerator _generator;

        public SourceGenerator(IRandomNumbersGenerator generator)
        {
            _generator = generator;
        }

        ExpressionSource IMultipleCalculationSourceGenerator.GetExpressionSource() =>
            new ExpressionSource
            {
                Numbers = _generator.Source(1, 99).Take(4).ToArray(),
                Operations = _generator.Source(1, 2).Take(3).Cast<Operation>().ToArray(),
            };

        DivisionsSource ICalculationSourceGenerator.GetDivisionsSource() =>
            new DivisionsSource
            {
                Number1 = _generator.Next(100, 9999),
                Number2 = _generator.Next(2, 9),
            };

        MultiplicationsSource ICalculationSourceGenerator.GetMultiplicationsSource() =>
            new MultiplicationsSource
            {
                Number1 = _generator.Next(100, 999),
                Number2 = _generator.Next(2, 9),
                IsValidResult = _generator.TrueOrFalse()
            };

        SubtractionsSource ICalculationSourceGenerator.GetSubtractionsSource()
        {
            var number1 = _generator.Next(101, 999);
            var number2 = _generator.Next(100, number1 - 1); // number2 must be smaller then number1
            return new SubtractionsSource
            {
                Number1 = number1,
                Number2 = number2,
                IsValidResult = _generator.TrueOrFalse()
            };
        }

        SumsSource ICalculationSourceGenerator.GetSumSource() =>
            new SumsSource
            {
                Numbers = _generator.Source(10, 99).Take(3).ToArray(),
                IsValidResult = _generator.TrueOrFalse()
            };

        DuplicatesSource ISeriesSourceGenerator.GetDuplicatesSource() =>
            new DuplicatesSource
            {
                Set = Enumerable.Range(0, 4)
                            .Select(__ => _generator.Source(0, 9).Take(4).ToArray())
                            .ToArray()
            };

        SearchSource ISeriesSourceGenerator.GetSearchSource()
        {
            var array = _generator.Source(0, 9).Take(625).ToArray();
            var numbers = _generator.Source(0, 624).Take(4).Select(x => array[x]).ToArray();

            return new SearchSource
            {
                Array = array,
                Numbers = numbers
            };
        }

        ExactSearchSource ISeriesSourceGenerator.GetExactSearchSource()
        {
            var array = _generator.Source(0, 9).Take(625).ToArray();
            var pairs = _generator.Source(0, 623).Take(2).Select(x => new int[] { array[x], array[x + 1] }).ToArray();

            return new ExactSearchSource
            {
                Array = array,
                Pairs = pairs
            };
        }
    }
}
