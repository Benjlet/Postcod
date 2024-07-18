using Microsoft.Extensions.Configuration;

namespace Postcod.ExampleFunction
{
    public class Configuration
    {
        private readonly IConfiguration _configuration;

        public string PostcodeServiceUrl => _configuration["POSTCODE_SERVICE_URL"];
        public int PostcodeServiceTimeout => int.TryParse(_configuration["POSTCODE_SERVICE_TIMEOUT_MILLISECONDS"], out int timeout) && timeout > 0 ? timeout : 10000;

        public Configuration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
