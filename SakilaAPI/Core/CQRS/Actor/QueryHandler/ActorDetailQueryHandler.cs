using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.Base;
using SakilaAPI.Core.CQRS.Actor.Query;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Models.Actor;

namespace SakilaAPI.Core.CQRS.Actor.QueryHandler
{
    /// <summary>
    /// ActorDetailQueryHandler
    /// </summary>
    public class ActorDetailQueryHandler : BaseHandler, IRequestHandler<ActorDetailQuery, ActorModel>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="dataContext"></param>
        public ActorDetailQueryHandler(IMapper mapper, DataContext dataContext) : base(mapper, dataContext)
        {
        }

        /// <summary>
        /// Xử lý lấy chi tiết diển viên
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ActorModel> Handle(ActorDetailQuery request, CancellationToken cancellationToken)
        {
            var actor = await _dataContext.Actors.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (actor == null)
            {
                throw new StatusSuccessException(StatusCodes.Status204NoContent, "Actor không tồn tại", request.Id.ToString());
            }
            return _mapper.Map<ActorModel>(actor);
        }
    }
}
