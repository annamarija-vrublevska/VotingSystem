using AutoMapper;

namespace VotingSystem.Api.Mappers
{
    public class VoterProfile : Profile
    {
        public VoterProfile()
        {
            CreateMap<Entities.Voter, Models.VoterDto>();
            CreateMap<Models.VoterDto, Entities.Voter>();
            CreateMap<Models.VoterForUpdateDto, Entities.Voter>();
        }
    }
}
