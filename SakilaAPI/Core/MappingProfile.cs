using AutoMapper;
using SakilaAPI.Core.Entities;
using SakilaAPI.Core.Models;
using SakilaAPI.Core.Models.Actor;

namespace SakilaAPI.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActorEntity, ActorModel>();

            CreateMap<ActorTaoMoiModel, ActorEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.LastUpdate, desc => desc.Ignore());

            CreateMap<ActorCapNhatModel, ActorEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.LastUpdate, desc => desc.Ignore());
        }
    }
}
