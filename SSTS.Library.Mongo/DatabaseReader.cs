using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using SSTS.Library.Common.Data;

namespace SSTS.Library.Mongo
{
    public class DatabaseReader : IDatabaseReader
    {
        public IDatabaseConnection DatabaseConnection { get; private set; }

        public DatabaseReader(IDatabaseConnection databaseConnection)
        {
            this.DatabaseConnection = databaseConnection;
        }

        public dynamic Read(Dictionary<string, object> filter)
        {
            var filterBSON = new BsonDocument(filter);

            var client = new MongoClient(this.DatabaseConnection.ConnectionString);
            var database = client.GetDatabase(this.DatabaseConnection.DatabaseName);
            var collection = database.GetCollection<dynamic>(this.DatabaseConnection.CollectionName);

            return collection.FindSync(filterBSON).FirstOrDefault();
        }
    }
}
