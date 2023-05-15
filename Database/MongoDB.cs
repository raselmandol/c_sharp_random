using System;
using MongoDB.Driver;

class Program {
    static void Main(string[] args) {
        //connecting using the server details
        MongoClient client = new MongoClient("mongodb://localhost:27017");
        IMongoDatabase db = client.GetDatabase("mydatabase");

        IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("mycollection");

        //Inserting Data
        BsonDocument document = new BsonDocument {
            { "name", "Rasel" },
            { "age", 21 },
            { "city", "Shillong" }
        };
        collection.InsertOne(document);

        //Deleting Data
        collection.DeleteOne(document);
        
        //Closing Connection
        client = null;
    }
}
