using AutoMapper;
using Microsoft.OpenApi.Extensions;

namespace VotingSystem.Api.Mappers
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Entities.Item, Models.ItemDto>();
            CreateMap<Models.ItemForCreationDto, Entities.Item>();
            CreateMap<Models.ItemForUpdateDto, Entities.Item>();
        }
    }
}
