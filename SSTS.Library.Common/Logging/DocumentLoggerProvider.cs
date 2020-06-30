using Microsoft.Extensions.Logging;
using SSTS.Library.Common.Data;

namespace SSTS.Library.Common.Logging
{
    public class DocumentLoggerProvider : ILoggerProvider
    {
        public IDatabaseAccessFactory DatabaseAccessFactory { get; private set; }

        public DocumentLoggerProvider(IDatabaseAccessFactory databaseAccessFactory)
        {
            this.DatabaseAccessFactory = databaseAccessFactory;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DocumentLogger(categoryName, this.DatabaseAccessFactory.GetWriter("Logging"));
        }

        public void Dispose()
        {
        }
    }
}
