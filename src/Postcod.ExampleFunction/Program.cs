using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Postcod.ExampleFunction
{
    public class Program
    {
        private const string PostcodeNamedHttpClient = "PostcodeHttpClient";

        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<Configuration>();

                    // You can simply inject the following if you do not need to re-use the HttpClient and want to use the default timeout/URL:
                    //      services.AddScoped<IPostcodeLookupClient, PostcodeLookupClient>();
                    // The below is a full named HttpClient example.

                    services.AddHttpClient(PostcodeNamedHttpClient, (provider, client) =>
                    {
                        var config = provider.GetRequiredService<Configuration>();
                        client.BaseAddress = new Uri(config.PostcodeServiceUrl);
                        client.Timeout = TimeSpan.FromMilliseconds(config.PostcodeServiceTimeout);
                    });

                    services.AddScoped<IPostcodeLookupClient>(provider =>
                    {
                        IHttpClientFactory httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                        return new PostcodeLookupClient(httpClientFactory.CreateClient(PostcodeNamedHttpClient));
                    });
                })
                .Build();

            await host.RunAsync();
        }
    }
}
