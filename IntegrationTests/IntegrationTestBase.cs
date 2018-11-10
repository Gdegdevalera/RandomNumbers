using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Generators.Interfaces;

namespace IntegrationTests
{
    public class IntegrationTestBase
    {
        protected const string CalculationBaseUrl = "api/calculation/";
        protected const string SumsBaseUrl = CalculationBaseUrl + "sums";
        protected const string MultiplicationsBaseUrl = CalculationBaseUrl + "multiplications";
        protected const string SubtractionsBaseUrl = CalculationBaseUrl + "subtractions";
        protected const string DivisionsBaseUrl = CalculationBaseUrl + "divisions";

        protected const string MultipleCalculationBaseUrl = "api/multiplecalculation/";
        protected const string ExpressionsBaseUrl = MultipleCalculationBaseUrl + "expression";

        protected const string SeriesBaseUrl = "api/series/";
        protected const string DuplicatesBaseUrl = SeriesBaseUrl + "duplicates";
        protected const string MatchesBaseUrl = SeriesBaseUrl + "matches";
        protected const string SearchBaseUrl = SeriesBaseUrl + "search";
        protected const string ExactSearchBaseUrl = SeriesBaseUrl + "exactsearch";


        protected ICalculationSourceGenerator CalculationSourceGenerator =
            Substitute.For<ICalculationSourceGenerator>();

        protected IMultipleCalculationSourceGenerator MultipleCalculationSourceGenerator =
            Substitute.For<IMultipleCalculationSourceGenerator>();

        protected ISeriesSourceGenerator SeriesSourceGenerator =
            Substitute.For<ISeriesSourceGenerator>();

        protected readonly Lazy<TestServer> TestServer;

        public IntegrationTestBase()
        {
            TestServer = new Lazy<TestServer>(() => new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>()
                .UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    services.AddSingleton(CalculationSourceGenerator);
                    services.AddSingleton(MultipleCalculationSourceGenerator);
                    services.AddSingleton(SeriesSourceGenerator);
                })));
        }

        protected async Task<T> Deserialize<T>(HttpResponseMessage message)
        {
            var responseStr = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseStr);
        }
    }
}
