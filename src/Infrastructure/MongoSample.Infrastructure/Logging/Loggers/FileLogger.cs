using Microsoft.Extensions.Logging;
using MongoSample.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MongoSample.Infrastructure.Logging.Loggers
{
    public class FileLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel.GetHashCode() > 2;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel.GetHashCode() <= 2) return;

            const string filePath = "Log.json";
            var log = new LogStruct(logLevel, eventId, formatter(state, exception), exception);
            //Task.Run(() => WriteToFile(filePath, log));
            WriteToFile(filePath, log);
        }

        void WriteToFile(string filePath, LogStruct log)
        {
            List<LogStruct> logs = null;

            using (var file = File.Open(filePath, FileMode.OpenOrCreate))
            {
                var readed = string.Empty;
                using (var reader = new StreamReader(file))
                    readed = reader.ReadToEnd();

                logs = JsonConvert.DeserializeObject<List<LogStruct>>(readed);
            }

            if (logs == null) logs = new List<LogStruct>();

            logs.Add(log);
            var logsSerialized = JsonConvert.SerializeObject(logs);

            using (var writer = File.AppendText(filePath))
            {
                writer.Write(logsSerialized);
                writer.Flush();
                writer.Close();
            }

        }

        [Serializable]
        public class LogStruct
        {
            [JsonProperty(PropertyName = " @timeStamp")]
            public string TimeStamp => DateTime.Now.ToString("G");
            public string LogLevel { get; }
            public Event LogEvent { get; }
            public string Formatted { get; }
            public LogException FiredException { get; }

            public LogStruct(LogLevel logLevel, EventId eventId, string formatted, Exception exception)
            {
                LogLevel = logLevel.ToString();
                LogEvent = new Event(eventId.Id, eventId.Name);
                Formatted = formatted;
                FiredException = new LogException(exception);
            }
            [Serializable]
            public class Event
            {
                public int Id { get; set; }
                public string Name { get; set; }

                public Event(int id, string name)
                {
                    Id = id;
                    Name = name;
                }
            }
            [Serializable]
            public sealed class LogException
            {
                public string Name { get; }
                public string StackTrace { get; }
                public string Source { get; set; }
                public string Message { get; }
                public LogException InnerException { get; }
                public IDictionary Data { get; }
                public LogException(Exception e)
                {
                    Name = e?.GetType()?.Name;
                    Message = e.Message;
                    StackTrace = e.StackTrace;
                    Source = e.Source;
                    Data = e.Data;
                    if (e.InnerException != null)
                        InnerException = new LogException(e.InnerException);
                }
                [JsonConstructor]
                public LogException(LogException e)
                {
                    Name = e.GetType().Name;
                    Message = e.Message;
                    StackTrace = e.StackTrace;
                    Source = e.Source;
                    Data = e.Data;
                    if (e.InnerException != null)
                        InnerException = new LogException(e.InnerException);
                }
            }
        }
    }
}
