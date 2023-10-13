using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila_B.Core.CQRS.Actor.Query;
using Sakila_B.Core.Entities;
using Sakila_B.Core.Models;
using Sakila_B.Core.Models.Actor;

namespace Sakila_B.Core.CQRS.Actor.QueryHandler
{
    public class ActorListQueryHandler : BaseHandler, IRequestHandler<ActorListQuery, List<ActorModel>>
    {
        public ActorListQueryHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<List<ActorModel>> Handle(ActorListQuery request, CancellationToken cancellationToken)
        {
            return await _dataContext.Actors.Take(request.soLuong).ProjectTo<ActorModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
