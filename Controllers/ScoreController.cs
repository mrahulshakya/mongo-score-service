using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.LeaderBoard.Service.Database;
using Api.LeaderBoard.Service.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.LeaderBoard.Service.Controllers
{

    [ApiController]
    [Produces("application/json")]
    public class ScoreController : ControllerBase
    {
        public ScoreController(IScoreDbRepository scoreDbRepository, IMapper mapper)
        {
            ScoreDbRepository = scoreDbRepository;
            Mapper = mapper;
        }

        public IScoreDbRepository ScoreDbRepository { get; }
        public IMapper Mapper { get; }

        // GET api/values
        [HttpGet]
        [Route("/scores/")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetHighetScoresResponse))]
        public async Task<IActionResult> GetHighestScores()
        {
            try
            {
                var scoreModel = await this.ScoreDbRepository.GetHighScore().ConfigureAwait(false);
                var scoreDto = this.Mapper.Map<IList<ScoreDto>>(scoreModel);
                return await Task.FromResult(Ok(new GetHighetScoresResponse
                {
                    Scores = scoreDto
                })
                );
            }
            catch(Exception ex)
            {
                throw;
            } 
        }

        // GET api/values/5
        [HttpGet]
        [Route("/scores/{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetScoreResponse))]
        public async Task<IActionResult> Get([FromRoute]string userId)
        {
            var score = await this.ScoreDbRepository.GetScore(userId).ConfigureAwait(false);

            var scoreDto = this.Mapper.Map<ScoreDto>(score);
            if (scoreDto == null)
            {
                return await Task.FromResult(NoContent());
            }

            return await Task.FromResult(Ok(new GetScoreResponse
            {
                Score = scoreDto
            }));
        }

        // POST api/values
        [HttpPost]
        [Route("/scores/")]
        //[ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(CreatedAtActionResult))]
        //[SwaggerOperation("Post")]
        public async Task<IActionResult> UpdateScore([FromBody] UpdateScoreRequest request)
        {
            if (!ModelState.IsValid)
            {
                return await Task.FromResult(BadRequest(ModelState));
            }

            var score = await this.ScoreDbRepository.GetScore(request.Score.UserId).ConfigureAwait(false);
            var scoreDto = this.Mapper.Map<ScoreDto>(score);
            if (scoreDto != null && scoreDto.Score > request.Score.Score)
            {
                return await Task.FromResult(BadRequest(new { Error = "Higher score already  exists!" }));
            }

            var scoreModel = new ScoreMongoModel
            {
                Id = request.Score.UserId,
                Score = request.Score.Score,
                UserName = request.Score.UserName
            };

            if (score == null)
            {
                await this.ScoreDbRepository.AddScore(scoreModel);
            } else
            {
                await this.ScoreDbRepository.UpdateScore(scoreModel).ConfigureAwait(false);
            }
         
            return await Task.FromResult(Created("/scores/{userId}", request));
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("/scores/{userId}")]
        public async Task<IActionResult> Delete([FromRoute] string userId)
        {

            var score = await this.ScoreDbRepository.GetScore(userId).ConfigureAwait(false);
       
            if (score == null)
            {
                return await Task.FromResult(NoContent());
            }

            await this.ScoreDbRepository.RemoveScore(score);
            return await Task.FromResult(Accepted());
        }
    }
}
