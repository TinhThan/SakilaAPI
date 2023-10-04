using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.Contants;
using SakilaAPI.Core.CQRS.Actor.Command;
using SakilaAPI.Core.Exceptions;

namespace SakilaAPI.Core.CQRS.Actor.CommandHandler
{
    public class CapNhatActorQueryHandler : BaseHandler, IRequestHandler<CapNhatActorQuery, string>
    {
        public CapNhatActorQueryHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<string> Handle(CapNhatActorQuery request, CancellationToken cancellationToken)
        {
            var actor = await _dataContext.Actors.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (actor.LastUpdate != request.CapNhatModel.LastUpdate)
            {
                throw new StatusSuccessException(MessageSystem.VERSION_UPDATE, new object[] { actor.LastUpdate });
            }
            var actorUpdate = _mapper.Map(request.CapNhatModel, actor);
            actorUpdate.LastUpdate = DateTime.Now;

            try
            {
                _dataContext.Actors.Update(actorUpdate);
                var resultUpdate = await _dataContext.SaveChangesAsync(cancellationToken);
                if (resultUpdate > 0)
                {
                    return MessageSystem.UPDATE_SUCCESS;
                }
                throw new StatusServerErrorException(MessageSystem.UPDATE_FAIL);
            }
            catch (Exception ex)
            {
                throw new StatusServerErrorException(MessageSystem.UPDATE_FAIL, ex.Message);
            }
        }
    }
}
