using System;
using Microsoft.Extensions.Logging;

namespace SSTS.Library.Common.Logging
{
    public partial class DocumentLogger
    {
        public class ExceptionEntry : ILogEntry
        {
            public EventId EventId { get; set; }

            public string Category { get; set; }

            public LogLevel LogLevel { get; set; }

            public DateTime CreatedDateTime { get; set; }

            public string Exception { get; set; }
        }
    }
}
