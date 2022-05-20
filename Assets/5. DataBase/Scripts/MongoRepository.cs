using System.Collections.Generic;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _5._DataBase.Scripts
{
    public class MongoRepository : IRepository
    {
        private static readonly MongoClient _client = new("mongodb://localhost:27017");
        private static readonly IMongoDatabase _database = _client.GetDatabase("ClickerDonut");
        private static readonly IMongoCollection<PlayerData> _playerCollection =
            _database.GetCollection<PlayerData>("players");
        
        public IEnumerable<PlayerData> GetAll() => _playerCollection.Find(x => true).ToList().ToArray();


        public PlayerData GetById(ObjectId id)
        {
            return _playerCollection
                .Find(Builders<PlayerData>
                    .Filter
                    .Eq("Id", id))
                .SingleOrDefault();
        }

        public void Create(PlayerData item)
        {
            if (HasAccount(ObjectId.Parse(item.Id)))
                return;

            _playerCollection.InsertOne(item);
        }

        public void Update(PlayerData item)
        {
            _playerCollection.ReplaceOne(Builders<PlayerData>
                .Filter
                .Eq("Id", item.Id), item);
        }

        public void Delete(ObjectId id)
        {
            _playerCollection.DeleteOne(Builders<PlayerData>
                .Filter
                .Eq("Id", id));
        }

        public bool HasAccount(ObjectId id) => GetById(id) != null;
        

        public PlayerData[] GetTopPlayerDonuts()
        {
            var result = _playerCollection.Aggregate(
                PipelineDefinition<PlayerData, PlayerData>.Create(
                    new BsonDocument("$sort",
                        new BsonDocument("donut", -1)))).ToList();

            return result.ToArray();
        }

        public PlayerData[] GetTopPlayerClicks()
        {
            var result = _playerCollection.Aggregate(
                PipelineDefinition<PlayerData, PlayerData>.Create(
                    new BsonDocument("$sort",
                        new BsonDocument("statisticsData.Clicks", -1)))).ToList();

            return result.ToArray();
        }

        public PlayerData[] GetTopPlayerDps()
        {
            var result = _playerCollection.Aggregate(
                PipelineDefinition<PlayerData, PlayerData>.Create(
                    new BsonDocument("$sort",
                        new BsonDocument("donutPerSecond", -1)))).ToList();

            return result.ToArray();
        }
        

        public bool Connection()
        {
            return _database.RunCommandAsync((Command<BsonDocument>) "{ping:1}").Wait(1000);
        }
    }
}