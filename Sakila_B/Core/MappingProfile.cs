using AutoMapper;
using Sakila_B.Core.Entities;
using Sakila_B.Core.Models;
using Sakila_B.Core.Models.Actor;

namespace Sakila_B.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActorEntity, ActorModel>();

            CreateMap<ActorTaoMoiModel, ActorEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.FilmActors, desc => desc.Ignore())
                .ForMember(src => src.LastUpdate, desc => desc.Ignore());

            CreateMap<ActorCapNhatModel, ActorEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.FilmActors, desc => desc.Ignore())
                .ForMember(src => src.LastUpdate, desc => desc.Ignore());
        }
    }
}
