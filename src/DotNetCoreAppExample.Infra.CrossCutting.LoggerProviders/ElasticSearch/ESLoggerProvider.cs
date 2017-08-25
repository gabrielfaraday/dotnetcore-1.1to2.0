using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreAppExample.Infra.CrossCutting.LoggerProviders.ElasticSearch
{
    public class ESLoggerProvider : ILoggerProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ESClientProvider _esClient;
        private readonly FilterLoggerSettings _filter;

        public ESLoggerProvider(IServiceProvider serviceProvider)
        {
            _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();

            _esClient = serviceProvider.GetService<ESClientProvider>();
            _esClient.EnsureIndexWithMapping<LogEntry>();

            _filter = new FilterLoggerSettings
            {
                {"*", _esClient.LogLevel}
            };
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new ESLogger(_esClient, _httpContextAccessor, categoryName, FindLevel(categoryName));
        }

        private LogLevel FindLevel(string categoryName)
        {
            var def = LogLevel.Warning;
            foreach (var s in _filter.Switches)
            {
                if (categoryName.Contains(s.Key)) return s.Value;
                if (s.Key == "*") def = s.Value;
            }

            return def;
        }

        public void Dispose()
        {
            // Nothing to do
        }
    }
}
