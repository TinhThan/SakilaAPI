using AutoMapper;
using Sakila_B.Core.Entities;
using Sakila_B.Core.Models.Film;

namespace Sakila_B.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<FilmEntity, FilmModel>();
        }
    }
}
