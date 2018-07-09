using Assessment.Domain.Contracts.Models;
using Assessment.Infrastructure.Contracts;
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Infrastructure.Impl
{
    public class HttpProxy : IHttpProxy
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HttpProxy> _logger;

        public HttpProxy(IMemoryCache cache, IConfiguration configuration, ILogger<HttpProxy> logger)
        {
            _cache = cache;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Client> GetCustomersAsync(CancellationToken cancellationToken)
        {
            return await GetAsync<Client>(_configuration.GetSection("settings:endpoints:customer").Value, cancellationToken);
        }

        public async Task<PolicyList> GetPoliciesAsync(CancellationToken cancellationToken)
        {
            return await GetAsync<PolicyList>(_configuration.GetSection("settings:endpoints:policy").Value, cancellationToken);
        }

        private async Task<TResult> GetAsync<TResult>(string url, CancellationToken cancellationToken)
        {
            if (!_cache.TryGetValue(nameof(TResult), out TResult result))
            {
                _logger.LogInformation($"Call url: {url}");

                result = await url.GetJsonAsync<TResult>(cancellationToken);

                var minutes = int.Parse(_configuration.GetSection("settings:cache:expirationtimeminutes").Value);

                _cache.Set(nameof(TResult), result, TimeSpan.FromMinutes(minutes));
            }

            return result;
        }
    }
}
