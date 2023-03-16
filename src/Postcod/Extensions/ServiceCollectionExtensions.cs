using Postcod.Abstractions;
using Postcod.Implementation;
using Postcod.Implementation.Abstractions;
using Postcod.Implementation.LookupServices.PostcodesIO;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;

namespace Postcod.Extensions
{
    /// <summary>
    /// Extensions for IServiceCollection Dependency Injection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Implements IPostcodeLookupService into the service collection, including all required dependencies.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="timeoutMilliseconds">The timeout, in milliseconds, of the call to the Postcodes IO service.</param>
        /// <returns></returns>
        public static IServiceCollection AddPostcodesIOLookup(this IServiceCollection services, long timeoutMilliseconds = 10000)
        {
            services.AddPostcodesLookup(new HttpClientConfig()
            {
                TimeoutMilliseconds = timeoutMilliseconds,
                BaseAddress = "https://postcodes.io/",
                RetryCount = 3
            });

            services.AddTransient<IPostcodeLookupService, PostcodesIOService>();
            return services;
        }

        private static IServiceCollection AddPostcodesLookup(this IServiceCollection services, HttpClientConfig clientConfig)
        {
            services.AddHttpClient(nameof(HttpClientWrapper), (provider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(clientConfig.BaseAddress);
                httpClient.Timeout = TimeSpan.FromMilliseconds(clientConfig.TimeoutMilliseconds);
            }).AddPolicyHandler(
                HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(clientConfig.RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>(provider =>
            {
                var factory = provider.GetRequiredService<IHttpClientFactory>();
                return new HttpClientWrapper(factory);
            });

            services.AddTransient<IPostcodeLookupClient, PostcodeLookupClient>();
            services.AddSingleton<ILocationHelper, LocationHelper>();

            return services;
        }

        private struct HttpClientConfig
        {
            internal int RetryCount { get; set; }
            internal string BaseAddress { get; set; }
            internal long TimeoutMilliseconds { get; set; }
        }
    }
}
