using System.ComponentModel.DataAnnotations;

namespace Api.LeaderBoard.Service.Models
{
    public class SetScoreRequest
    {
        public ScoreDto Score { get; set; }
    }

    public class UpdateScoreRequest
    {
        public ScoreDto Score { get; set; }
    }
}
