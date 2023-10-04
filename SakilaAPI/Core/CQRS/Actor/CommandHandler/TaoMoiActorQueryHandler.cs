using AutoMapper;
using MediatR;
using SakilaAPI.Core.Contants;
using SakilaAPI.Core.CQRS.Actor.Command;
using SakilaAPI.Core.Entities;
using SakilaAPI.Core.Exceptions;

namespace SakilaAPI.Core.CQRS.Actor.CommandHandler
{
    public class TaoMoiActorQueryHandler : BaseHandler, IRequestHandler<TaoMoiActorQuery, string>
    {
        public TaoMoiActorQueryHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<string> Handle(TaoMoiActorQuery request, CancellationToken cancellationToken)
        {
            var actor = _mapper.Map<ActorEntity>(request.ActorTaoMoiModel);
            actor.LastUpdate = DateTime.Now;

            try
            {
                await _dataContext.AddAsync(actor, cancellationToken);
                var resultUpdate = await _dataContext.SaveChangesAsync(cancellationToken);
                if (resultUpdate > 0)
                {
                    return MessageSystem.ADD_SUCCESS;
                }
                throw new StatusServerErrorException(MessageSystem.ADD_FAIL);
            }
            catch (Exception ex)
            {
                throw new StatusServerErrorException(MessageSystem.UPDATE_FAIL, ex.Message);
            }
        }
    }
}
