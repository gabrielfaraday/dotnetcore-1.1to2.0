using Microsoft.Extensions.Options;
using Nest;
using System;

namespace DotNetCoreAppExample.Infra.CrossCutting.LoggerProviders.ElasticSearch
{
    public class ESClientProvider
    {
        public ElasticClient Client { get; private set; }
        public string DefaultIndex { get; private set; }
        public Microsoft.Extensions.Logging.LogLevel LogLevel { get; private set; }

        public ESClientProvider(IOptions<ESClientProviderConfig> options)
        {
            var settings = new ConnectionSettings(new Uri(options.Value.Uri))
                .InferMappingFor<LogEntry>(i => i
                    .IndexName(options.Value.DefaultIndex)
                    .TypeName(options.Value.DefaultType)
                );

            if (!string.IsNullOrEmpty(options.Value.UserName) && !string.IsNullOrEmpty(options.Value.Password))
                settings.BasicAuthentication(options.Value.UserName, options.Value.Password);

            Client = new ElasticClient(settings);
            DefaultIndex = options.Value.DefaultIndex;

            if (Enum.TryParse(options.Value.LogLevel, out Microsoft.Extensions.Logging.LogLevel level))
                LogLevel = level;
            else
                LogLevel = Microsoft.Extensions.Logging.LogLevel.Warning;
        }

        public void EnsureIndexWithMapping<T>(string indexName = null, Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> customMapping = null) where T : class
        {
            if (string.IsNullOrEmpty(indexName))
                indexName = DefaultIndex;

            if (!Client.ConnectionSettings.DefaultIndices.ContainsKey(typeof(T)))
                Client.ConnectionSettings.DefaultIndices.Add(typeof(T), indexName);

            var indexExistsResponse = Client.IndexExists(new IndexExistsRequest(indexName));
            if (!indexExistsResponse.IsValid)
                throw new InvalidOperationException(indexExistsResponse.DebugInformation);

            if (indexExistsResponse.Exists)
                return;

            var createIndexRes = Client.CreateIndex(indexName);
            if (!createIndexRes.IsValid)
                throw new InvalidOperationException(createIndexRes.DebugInformation);

            var res = Client.Map<T>(m =>
            {
                m.AutoMap().Index(indexName);

                if (customMapping != null)
                    m = customMapping(m);

                return m;
            });

            if (!res.IsValid)
                throw new InvalidOperationException(res.DebugInformation);
        }
    }
}
