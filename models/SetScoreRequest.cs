using System.ComponentModel.DataAnnotations;

namespace Api.LeaderBoard.Service.Models
{
    public class SetScoreRequest
    {
        public ScoreDto Score { get; set; }
    }

    public class UpdateScoreRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Score { get; set; }

        public string UserName { get; set; }
    }
}
