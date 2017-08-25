using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace DotNetCoreAppExample.Infra.CrossCutting.LoggerProviders
{
    public class LogEntry
    {
        public DateTime DateTime { get; set; }
        public EventId EventId { get; set; }
        public string LogLevel { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }

        public string TraceIdentifier { get; set; }
        public string UserName { get; set; }
        public string ContentType { get; set; }
        public string Host { get; set; }
        public string Method { get; set; }
        public string Protocol { get; set; }
        public string Scheme { get; set; }
        public string Path { get; set; }
        public string PathBase { get; set; }
        public string QueryString { get; set; }
        public long? ContentLength { get; set; }
        public bool IsHttps { get; set; }
        public IRequestCookieCollection Cookies { get; set; }
        public IHeaderDictionary Headers { get; set; }

        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string Exception { get; set; }
        public bool HasException { get { return Exception != null; } }
        public string StackTrace { get; set; }
    }
}
