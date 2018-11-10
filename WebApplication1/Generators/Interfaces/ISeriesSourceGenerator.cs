using WebApplication1.Models.Source;

namespace WebApplication1.Generators.Interfaces
{
    public interface ISeriesSourceGenerator
    {
        DuplicatesSource GetDuplicatesSource();
        SearchSource GetSearchSource();
        ExactSearchSource GetExactSearchSource();
    }
}
