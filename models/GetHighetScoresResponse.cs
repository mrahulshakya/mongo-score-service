using System.Collections.Generic;

namespace Api.LeaderBoard.Service.Models
{
    public class GetHighetScoresResponse
    {
        public IList<ScoreDto> Scores { get; set; }
    }
}
