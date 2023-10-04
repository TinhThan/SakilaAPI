using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.CQRS.Actor.Query;
using SakilaAPI.Core.Entities;
using SakilaAPI.Core.Models;

namespace SakilaAPI.Core.CQRS.Actor.QueryHandler
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
