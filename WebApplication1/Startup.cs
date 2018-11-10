using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Generators;
using WebApplication1.Generators.Interfaces;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCommonServices(services);

            services.AddScoped<ICalculationSourceGenerator, SourceGenerator>();
            services.AddScoped<IMultipleCalculationSourceGenerator, SourceGenerator>();
            services.AddScoped<ISeriesSourceGenerator, SourceGenerator>();
        }

        protected void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddScoped<IRandomNumbersGenerator, RandomNumbersGenerator>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
