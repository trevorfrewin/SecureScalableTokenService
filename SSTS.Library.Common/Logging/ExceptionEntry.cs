using System;
using Microsoft.Extensions.Logging;

namespace SSTS.Library.Common.Logging
{
    public partial class DocumentLogger
    {
        public class ExceptionLogEntry : LogEntry
        {
            public string Exception { get; set; }
        }
    }
}
