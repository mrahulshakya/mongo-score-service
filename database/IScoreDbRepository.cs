using Api.LeaderBoard.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.LeaderBoard.Service.Database
{
    public interface IScoreDbRepository
    {
         Task<ScoreMongoModel> GetScore(string userId);

        Task AddScore(ScoreMongoModel score);

        Task<IList<ScoreMongoModel>> GetHighScore();


        Task<bool> UpdateScore(ScoreMongoModel score);

        Task<long> RemoveScore(ScoreMongoModel score);
    }
}
