using MediatR;
using SakilaAPI.Core.Models.Actor;

namespace SakilaAPI.Core.CQRS.Actor.Query
{
    /// <summary>
    /// Actor list query
    /// </summary>
    public class ActorListQuery : IRequest<List<ActorModel>>
    {
        public int soLuong { get; set; }
    }
}
