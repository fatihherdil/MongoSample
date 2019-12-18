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

            using (var file = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                List<LogStruct> logs = null;

                var readed = string.Empty;
                using (var reader = new StreamReader(file))
                    readed = reader.ReadToEnd();

                try { logs = JsonConvert.DeserializeObject<List<LogStruct>>(readed); }
                catch (Exception e) { }

                if (logs == null) logs = new List<LogStruct>();

                logs.Add(log);
                var logsSerialized = JsonConvert.SerializeObject(logs);

                using (var writer = new StreamWriter(file))
                {
                    writer.Write(logsSerialized);
                    writer.Flush();
                    writer.Close();
                }
                file.Close();
            }
        }

        [Serializable]
        public class LogStruct
        {
            public string LogLevel { get; }
            public Event LogEvent { get; }
            public string Formatted { get; }
            public LogException Exception { get; }

            public LogStruct(LogLevel logLevel, EventId eventId, string formatted, Exception exception)
            {
                LogLevel = logLevel.ToString();
                LogEvent = new Event(eventId.Id, eventId.Name);
                Formatted = formatted;
                Exception = new LogException(exception);
            }

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
            public class LogException
            {
                public virtual string Name { get; }
                public virtual string StackTrace { get; }
                public virtual string Source { get; set; }
                public virtual string Message { get; }
                public Exception InnerException { get; }
                public int HResult { get; protected set; }
                public virtual IDictionary Data { get; }
                //public MethodBase TargetSite { get; }
                public virtual string HelpLink { get; set; }
                public LogException(Exception e)
                {
                    Name = e.GetType().Name;
                    Message = e.Message;
                    StackTrace = e.StackTrace;
                    Source = e.Source;
                    InnerException = e.InnerException;
                    HResult = e.HResult;
                    Data = e.Data;
                    //TargetSite = e.TargetSite;
                    HelpLink = e.HelpLink;
                }
            }
        }
    }
}
