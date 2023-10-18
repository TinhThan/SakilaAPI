using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.CQRS.Actor.Query;
using SakilaAPI.Core.Entities;
using SakilaAPI.Core.Models;
using SakilaAPI.Core.Models.Actor;
using SakilaAPI.ExternalService;

namespace SakilaAPI.Core.CQRS.Actor.QueryHandler
{
    public class ActorListQueryHandler : BaseHandler, IRequestHandler<ActorListQuery, List<ActorModel>>
    {
        private readonly IFilmService _filmService;
        public ActorListQueryHandler(DataContext dataContext, IMapper mapper, IFilmService filmService) : base(dataContext, mapper)
        {
            _filmService = filmService;
        }

        public async Task<List<ActorModel>> Handle(ActorListQuery request, CancellationToken cancellationToken)
        {
            var actorEntitys = await _dataContext.Actors.Take(request.soLuong).ToListAsync(cancellationToken);
            var filmIds = actorEntitys.SelectMany(x => x.FilmActors.Select(t => t.FilmId)).Distinct().ToArray();

            var filmModels = await _filmService.DanhSachFilmByIds(filmIds);

            var actorModels = _mapper.Map<List<ActorModel>>(actorEntitys);

            foreach (var item in actorModels)
            {
                var actorEntity = actorEntitys.FirstOrDefault(t => t.Id == item.Id);
                item.Films = filmModels.Where(x => actorEntity.FilmActors.Any(t => t.FilmId == x.FilmId)).ToList();
            }

            return await _dataContext.Actors.Take(request.soLuong).ProjectTo<ActorModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
