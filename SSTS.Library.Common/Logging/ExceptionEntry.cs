namespace SSTS.Library.Common.Logging
{
    public partial class DocumentLogger
    {
        public class ExceptionLogEntry : LogEntry
        {
            public required string Exception { get; set; }
        }
    }
}
