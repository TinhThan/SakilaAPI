using MediatR;
using Sakila_B.Core.Models.Actor;

namespace Sakila_B.Core.CQRS.Actor.Query
{
    /// <summary>
    /// Actor list query
    /// </summary>
    public class ActorListQuery : IRequest<List<ActorModel>>
    {
        public int soLuong { get; set; }
    }
}
