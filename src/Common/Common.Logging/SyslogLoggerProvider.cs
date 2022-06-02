using Microsoft.Extensions.Logging;

namespace Common.Logging
{
    // Source: https://gunnarpeipman.com/aspnet-core-syslog/
    public class SyslogLoggerProvider : ILoggerProvider
    {
        private readonly string _host;
        private readonly int _port;
        private readonly Func<string, LogLevel, bool>? _filter;

        public SyslogLoggerProvider(string host, int port, Func<string, LogLevel, bool>? filter)
        {
            _host = host;
            _port = port;
            _filter = filter;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new SyslogLogger(categoryName, _host, _port, _filter);
        }

        public void Dispose()
        {
            
        }
    }
}
