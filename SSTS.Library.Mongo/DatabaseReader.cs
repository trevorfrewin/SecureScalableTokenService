using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using SSTS.Library.Common.Data;

namespace SSTS.Library.Mongo
{
    public class DatabaseReader : IDatabaseReader
    {
        public dynamic Read(IDatabaseConnection connection, Dictionary<string, object> filter)
        {
            var filterBSON = new BsonDocument(filter);

            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DatabaseName);
            var collection = database.GetCollection<dynamic>(connection.CollectionName);

            return collection.FindSync(filterBSON).FirstOrDefault();
        }
    }
}
