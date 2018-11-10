using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebApplication1;

namespace IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);

            var assembly = typeof(Startup).GetTypeInfo().Assembly;
            var part = new AssemblyPart(assembly);
            services.AddMvc().ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));
        }
    }
}
