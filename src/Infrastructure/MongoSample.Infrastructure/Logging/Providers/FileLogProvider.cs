using Microsoft.Extensions.Logging;
using MongoSample.Infrastructure.Logging.Loggers;

namespace MongoSample.Infrastructure.Logging.Providers
{
    public class FileLogProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger();
        }

        public void Dispose()
        {
        }
    }
}
