using System;
using Microsoft.Extensions.Logging;

namespace SSTS.Library.Common.Logging
{
    public partial class DocumentLogger
    {
        public class LogEntry : ILogEntry
        {
            public EventId EventId { get; set; }

            public string MachineName { get; set; }

            public string Category { get; set; }

            public LogLevel LogLevel { get; set; }

            public DateTime CreatedDateTime { get; set; }

            public string Message { get; set; }
        }
    }
}
