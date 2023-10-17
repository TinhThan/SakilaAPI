using AutoMapper;
using Newtonsoft.Json;
using SakilaAPI.Core.Entities;
using SakilaAPI.Core.Models;
using SakilaAPI.Core.Models.Actor;
using SakilaAPI.Core.Models.User;

namespace SakilaAPI.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ActorEntity, ActorModel>()
                .ForMember(src => src.Films, desc => desc.Ignore())
                .ForMember(src => src.IdFilms, desc => desc.MapFrom(t => t.FilmActors.Select(x => x.FilmId)));

            CreateMap<ActorTaoMoiModel, ActorEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.FilmActors, desc => desc.Ignore())
                .ForMember(src => src.LastUpdate, desc => desc.Ignore());

            CreateMap<ActorCapNhatModel, ActorEntity>()
                .ForMember(src => src.Id, desc => desc.Ignore())
                .ForMember(src => src.FilmActors, desc => desc.Ignore())
                .ForMember(src => src.LastUpdate, desc => desc.Ignore());

            CreateMap<UserEntity, UserModel>()
                .ForMember(src => src.Permission, desc => desc.MapFrom(x => JsonConvert.DeserializeObject<List<string>>(x.Permission)))
                .ReverseMap().ForMember(src => src.RefreshTokenExpiryTime, desc => desc.Ignore());

            CreateMap<RegisterModel, UserEntity>()
                .ForMember(src => src.Password, desc => desc.Ignore())
                .ForMember(src => src.RefreshTokenExpiryTime, desc => desc.Ignore())
                .ForMember(src => src.Permission, desc => desc.MapFrom(x=> JsonConvert.SerializeObject(x.Permission)))
                .ForMember(src => src.RefreshToken, desc => desc.Ignore())
                .ForMember(src => src.LastUpdate, desc=> desc.Ignore());
        }
    }
}
