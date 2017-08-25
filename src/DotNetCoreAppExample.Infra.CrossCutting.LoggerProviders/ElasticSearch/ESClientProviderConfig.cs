namespace DotNetCoreAppExample.Infra.CrossCutting.LoggerProviders.ElasticSearch
{
    public class ESClientProviderConfig
    {
        public string Uri { get; set; }
        public string DefaultIndex { get; set; }
        public string DefaultType { get; set; }
        public string LogLevel { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
