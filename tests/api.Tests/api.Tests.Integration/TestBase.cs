using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using api.Interfaces.Repository;
using api.Repository;
using api.Interfaces;
using api.Services;
using System.Net.Http.Headers;


namespace api.Tests.api.Tests.Integration
{
    public class TestBase : IDisposable
    {
        public IServiceProvider Services { get; }
        
        public TestBase()
        {
            bool useRealApi = Environment.GetEnvironmentVariable("USE_REAL_API") == "true";

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables();

            if (useRealApi)
                configBuilder.AddJsonFile("appsettings.Development.json"); // only loaded if true

            var config = configBuilder.Build();

            var services = new ServiceCollection();
            
            services.AddScoped<ICVScannerService, CVScannerService>();
            services.AddScoped<IPdfReaderRepository, PdfReaderRepository>();
            services.AddSingleton<IConfiguration>(config);
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddHttpClient<IOpenAiRepository, OpenAiRepository>(client =>
            {
                client.BaseAddress = new Uri("https://api.openai.com/v1/");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", config["OpenAI:APIKey"]);
            });
            Services = services.BuildServiceProvider();
        }

        public void Dispose()
        {
        }
    }
}