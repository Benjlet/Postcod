using Postcod.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Postcod.ExampleFunction.Startup))]
namespace Postcod.ExampleFunction
{
    public class Startup : FunctionsStartup
    {
        public IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services.AddPostcodesIOLookup();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureServices(builder.Services);
        }
    }
}
