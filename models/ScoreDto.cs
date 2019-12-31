using System.ComponentModel.DataAnnotations;

namespace Api.LeaderBoard.Service.Models
{
    public class ScoreDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Score { get; set; }
    }
}
