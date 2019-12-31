using Api.LeaderBoard.Service.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.LeaderBoard.Service.Database
{
    public class ScoreDbRepository : MongoRepositoryBase, IScoreDbRepository
    { 
        public ScoreDbRepository() : base("mongodb://localhost:27017", "ScoreDB")
        {
            //var client = new MongoClient();
            //if (client != null)
            //    Database = client.GetDatabase("ScoreDB");
        }

        public async Task<ScoreMongoModel> GetScore(string userId)
        {
            return await GetOneAsync<ScoreMongoModel, string>(x => x.Id == userId).ConfigureAwait(false);
        }

        public async Task AddScore(ScoreMongoModel score)
        {
            await AddOneAsync<ScoreMongoModel, string>(score);
        }

        public async Task<IList<ScoreMongoModel>> GetHighScore()
        {
            return await GetSortedPaginatedAsync<ScoreMongoModel, string>(x=> true, x => x.Score, false)
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateScore(ScoreMongoModel score)
        {
            return await UpdateOneAsync<ScoreMongoModel,string>(score).ConfigureAwait(false);
        }

        public async Task<long> RemoveScore(ScoreMongoModel score)
        {
             return await DeleteOneAsync<ScoreMongoModel, string>(score).ConfigureAwait(false);
        }
    }
}
