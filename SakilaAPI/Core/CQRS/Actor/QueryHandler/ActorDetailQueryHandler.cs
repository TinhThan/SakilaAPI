using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.CQRS.Actor.Query;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Models;

namespace SakilaAPI.Core.CQRS.Actor.QueryHandler
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
            var actorModel = await _dataContext.Actors.ProjectTo<ActorModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (actorModel == null)
            {
                throw new StatusSuccessException(StatusCodes.Status204NoContent, "Actor không tồn tại", request.Id.ToString());
            }
            return actorModel;
        }
    }
}
