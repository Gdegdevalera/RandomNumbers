using WebApplication1.Models.Source;

namespace WebApplication1.Generators.Interfaces
{
    public interface ICalculationSourceGenerator
    {
        SumsSource GetSumSource();
        SubtractionsSource GetSubtractionsSource();
        MultiplicationsSource GetMultiplicationsSource();
        DivisionsSource GetDivisionsSource();
    }
}
