using System;
using AspNetCore.ServiceBenchmark.Grpc.Services;
using AspNetCore.ServiceBenchmark.TestClient.Interfaces.Services;
using AspNetCore.ServiceBenchmark.TestClient.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCore.ServiceBenchmark.TestClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    switch (hostContext.Configuration["Service:Type"])
                    {
                        case "Grpc":
                            services.AddGrpcClient<TestDataService.TestDataServiceClient>(o =>
                            {
                                o.Address = new Uri(hostContext.Configuration["Service:Url"]);
                            });
                            services.AddSingleton<ITestService, GrpcTestService>();
                            break;
                        case "Rest":
                            services.AddHttpClient<ITestService, RestTestService>((client) =>
                            {
                                client.BaseAddress = new Uri(hostContext.Configuration["Service:Url"]);
                            });
                            break;
                        default:
                            throw new NotSupportedException($"{hostContext.Configuration["Service:Type"]} is not a supported service type");
                    }
                    services.AddHostedService<Worker>();
                });
    }
}
