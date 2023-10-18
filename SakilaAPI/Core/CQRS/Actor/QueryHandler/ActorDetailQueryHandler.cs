using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.Base;
using SakilaAPI.Core.CQRS.Actor.Query;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Models.Actor;
using SakilaAPI.ExternalService;

namespace SakilaAPI.Core.CQRS.Actor.QueryHandler
{
    /// <summary>
    /// Actor Detail QueryHandler
    /// </summary>
    public class ActorDetailQueryHandler : BaseHandler, IRequestHandler<ActorDetailQuery, ActorModel>
    {
        private readonly IFilmService _filmService;
        public ActorDetailQueryHandler(DataContext dataContext, IMapper mapper, IFilmService filmService) : base(dataContext, mapper)
        {
            _filmService = filmService;
        }

        public async Task<ActorModel> Handle(ActorDetailQuery request, CancellationToken cancellationToken)
        {
            var actorEntity = await _dataContext.Actors.Include(t => t.FilmActors).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (actorEntity == null)
            {
                throw new StatusSuccessException(StatusCodes.Status204NoContent, "Actor không tồn tại", request.Id.ToString());
            }
            var actorModel = _mapper.Map<ActorModel>(actorEntity);
            actorModel.Films = await _filmService.DanhSachFilmByIds(actorEntity.FilmActors.Select(t => t.FilmId).Distinct().ToArray());
            return actorModel;
        }
    }
}
