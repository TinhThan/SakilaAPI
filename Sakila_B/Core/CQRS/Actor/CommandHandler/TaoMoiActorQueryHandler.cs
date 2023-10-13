using AutoMapper;
using MediatR;
using Sakila_B.Core.Contants;
using Sakila_B.Core.CQRS.Actor.Command;
using Sakila_B.Core.Entities;
using Sakila_B.Core.Exceptions;

namespace Sakila_B.Core.CQRS.Actor.CommandHandler
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
