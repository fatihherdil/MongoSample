using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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
                //Uncomment for Json.Net
                //var readed = string.Empty;
                //using (var reader = new StreamReader(file))
                //    readed = reader.ReadToEnd();

                try
                {
                    //logs = JsonConvert.DeserializeObject<List<LogStruct>>(readed); //Json.Net
                    logs = Utf8Json.JsonSerializer.Deserialize<List<LogStruct>>(file); //Utf8Json
                }
                catch (Exception) { }
            }

            if (logs == null) logs = new List<LogStruct>();

            logs.Add(log);
            var logsSerialized = Utf8Json.JsonSerializer.ToJsonString(logs); // JsonConvert.SerializeObject(logs); //Json.Net

            using (var writer = new StreamWriter(filePath, false))
            {
                writer.Write(logsSerialized);
                writer.Flush();
                writer.Close();
            }

        }

        #region Log Structure For Json
        [Serializable]
        public class LogStruct
        {
            [JsonProperty(PropertyName = " @timeStamp")]
            public string TimeStamp => DateTime.Now.ToString("G");
            public string LogLevel { get; set; }
            public Event LogEvent { get; set; }
            public string Formatted { get; set; }
            public LogException FiredException { get; set; }
            public LogStruct()
            {
            }
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
                public string Name { get; set; }
                public string StackTrace { get; set; }
                public string Source { get; set; }
                public string Message { get; set; }
                public LogException InnerException { get; set; }
                public IDictionary Data { get; set; }
                public LogException()
                {

                }
                public LogException(Exception e)
                {
                    if (e == null) return;
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
        #endregion
    }
}
