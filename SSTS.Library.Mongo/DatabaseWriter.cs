using MongoDB.Bson;
using MongoDB.Driver;
using SSTS.Library.Common.Data;

namespace SSTS.Library.Mongo
{
    public class DatabaseWriter : IDatabaseWriter
    {
        private class WrittenDocument
        {
            public ObjectId Id { get; private set; }
            public dynamic Document { get; private set; }

            public WrittenDocument(dynamic document)
            {
                Id = ObjectId.GenerateNewId();
                Document = document;
            }
        }

        public IDatabaseConnection DatabaseConnection { get; private set; }

        public DatabaseWriter(IDatabaseConnection databaseConnection)
        {
            this.DatabaseConnection = databaseConnection;
        }

        public void Write(dynamic document)
        {
            var client = new MongoClient(this.DatabaseConnection.ConnectionString);
            var database = client.GetDatabase(this.DatabaseConnection.DatabaseName);
            var collection = database.GetCollection<dynamic>(this.DatabaseConnection.CollectionName);

            collection.InsertOne(new WrittenDocument(document));
        }
    }
}
