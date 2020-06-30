using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SSTS.Library.Common.Data;

namespace SSTS.Library.Common.Logging
{
    public partial class DocumentLogger : ILogger
    {
        public class DocumentLoggerState : IDisposable
        {
            public ILogger Logger { get; private set; }

            public object InternalState { get; private set; }

            public DocumentLoggerState(ILogger logger, dynamic internalState)
            {
                this.InternalState = internalState;
                this.Logger = logger;
            }

            public void Dispose()
            {
                if (this.InternalState != null)
                {
                    this.Logger.LogInformation("EndScope: {0}", JsonConvert.SerializeObject(this.InternalState));
                }
            }
        }

        private IDatabaseWriter DatabaseWriter { get; set; }

        public string Category { get; private set; }

        public DocumentLogger(string category, IDatabaseWriter databaseWriter)
        {
            this.Category = category;
            this.DatabaseWriter = databaseWriter;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            ILogEntry document;

            if (exception == null)
            {
                document = new LogEntry()
                {
                    EventId = eventId,
                    Category = this.Category,
                    LogLevel = logLevel,
                    CreatedDateTime = DateTime.UtcNow,
                    Message = formatter(state, exception)
                };
            }
            else
            {
                var formattedException = new StringBuilder();

                formattedException.Append(state);
                formattedException.AppendLine(string.Format("\n[{0}] {1}", exception.GetType().Name, exception.Message));
                formattedException.AppendLine(exception.StackTrace);
                var innerException = exception.InnerException;
                while (innerException != null)
                {
                    formattedException.AppendLine(string.Format("\n[{0}] {1}", innerException.GetType().Name, innerException.Message));
                    innerException = innerException.InnerException;
                }

                document = new ExceptionEntry()
                {
                    EventId = eventId,
                    Category = this.Category,
                    LogLevel = logLevel,
                    CreatedDateTime = DateTime.UtcNow,
                    Exception = formattedException.ToString()
                };
            }

            this.DatabaseWriter.Write(document);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new DocumentLoggerState(this, state);
        }
    }
}
