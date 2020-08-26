using AspNetCore.ServiceBenchmark.Rest._3._1.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Utf8Json.Resolvers;

namespace AspNetCore.ServiceBenchmark.Rest._3._1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Configuration.GetValue<bool>("UseNewtonsoft"))
            {
                services.AddControllers().AddNewtonsoftJson();
            }
            else
            {
                var mvcBuilder = services.AddControllers();
                if (Configuration.GetValue<bool>("UseUtf8Json"))
                {
                    mvcBuilder.AddMvcOptions(option =>
                    {
                        option.OutputFormatters.Clear();
                        option.OutputFormatters.Add(new Utf8JsonOutputFormatter(StandardResolver.CamelCase));
                        option.InputFormatters.Clear();
                        option.InputFormatters.Add(new Utf8JsonInputFormatter(StandardResolver.CamelCase));
                    });
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
