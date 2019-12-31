using Api.LeaderBoard.Service.Models;
using AutoMapper;

namespace api_leaderboard_service
{
    public class ScoreProfile : Profile
    {
        public ScoreProfile()
        {
            CreateMap<ScoreDto, ScoreMongoModel>()
                 .ForMember(dest => dest.Id, m => m.MapFrom(src => src.UserId))
                 .ForMember(dest => dest.Version, m => m.Ignore())
                 .ReverseMap()
                 .ForMember(dest => dest.UserId, m => m.MapFrom(src => src.Id));
        }
    }
}
