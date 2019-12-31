using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;

namespace Api.LeaderBoard.Service.Models
{
    [CollectionName("Score")]
    public class ScoreMongoModel : IDocument<string>
    {
        public string UserName { get; set; }

        public int Score { get; set; }

        [BsonId]
        public string Id { get; set; }
        public int Version { get; set; }
    }
}
