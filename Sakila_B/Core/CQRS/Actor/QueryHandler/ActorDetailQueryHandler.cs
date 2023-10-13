using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila_B.Core.Base;
using Sakila_B.Core.CQRS.Actor.Query;
using Sakila_B.Core.Exceptions;
using Sakila_B.Core.Models.Actor;

namespace Sakila_B.Core.CQRS.Actor.QueryHandler
{
    /// <summary>
    /// Actor Detail QueryHandler
    /// </summary>
    public class ActorDetailQueryHandler : BaseHandler, IRequestHandler<ActorDetailQuery, ActorModel>
    {
        public ActorDetailQueryHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {

        }

        public async Task<ActorModel> Handle(ActorDetailQuery request, CancellationToken cancellationToken)
        {
            var actorFilm = await _dataContext.Actors.Where(t => t.Id == request.Id).Include(t => t.FilmActors).ToListAsync(cancellationToken);

            var actorModel = await _dataContext.Actors.ProjectTo<ActorModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (actorModel == null)
            {
                throw new StatusSuccessException(StatusCodes.Status204NoContent, "Actor không tồn tại", request.Id.ToString());
            }
            return actorModel;
        }
    }
}
