using AutoMapper;
<<<<<<< HEAD
using AutoMapper.QueryableExtensions;
=======
>>>>>>> 101f70ef9824a639cdf251d6c3d58e415c903b47
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.Base;
using SakilaAPI.Core.CQRS.Actor.Query;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Models.Actor;

namespace SakilaAPI.Core.CQRS.Actor.QueryHandler
{
    /// <summary>
    /// Actor Detail QueryHandler
    /// </summary>
    public class ActorDetailQueryHandler : BaseHandler, IRequestHandler<ActorDetailQuery, ActorModel>
    {
<<<<<<< HEAD
        public ActorDetailQueryHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
=======
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dataContext"></param>
        public ActorDetailQueryHandler(IMapper mapper, DataContext dataContext) : base(mapper, dataContext)
>>>>>>> 101f70ef9824a639cdf251d6c3d58e415c903b47
        {
        }

        public async Task<ActorModel> Handle(ActorDetailQuery request, CancellationToken cancellationToken)
        {
            var actorModel = await _dataContext.Actors.ProjectTo<ActorModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (actorModel == null)
            {
                throw new StatusSuccessException(StatusCodes.Status204NoContent, "Actor không tồn tại", request.Id.ToString());
            }
<<<<<<< HEAD
            return actorModel;
=======
            return _mapper.Map<ActorModel>(actor);
>>>>>>> 101f70ef9824a639cdf251d6c3d58e415c903b47
        }
    }
}
