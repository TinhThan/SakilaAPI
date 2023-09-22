using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.CQRS.Actor.Query;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Models;

namespace SakilaAPI.Core.CQRS.Actor.QueryHandler
{
    /// <summary>
    /// ActorDetailQueryHandler
    /// </summary>
    public class ActorDetailQueryHandler : IRequestHandler<ActorDetailQuery, ActorModel>
    {
        /// <summary>
        /// Khai báo datacontext
        /// </summary>
        private readonly DataContext _dataContext;

        /// <summary>
        /// Contructor ActorDetailQueryHandler
        /// </summary>
        /// <param name="dataContext"></param>
        public ActorDetailQueryHandler(DataContext dataContext)
        {
            _dataContext = dataContext;
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
            var actorModel = new ActorModel()
            {
                Id = actor.Id,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                LastUpdate = actor.LastUpdate
            };
            return actorModel;
        }
    }
}
