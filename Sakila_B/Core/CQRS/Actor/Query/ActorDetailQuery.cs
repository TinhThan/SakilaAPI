using MediatR;
using Sakila_B.Core.Models.Actor;

namespace Sakila_B.Core.CQRS.Actor.Query
{
    /// <summary>
    /// Chi tiết diển viên theo id
    /// </summary>
    public class ActorDetailQuery : IRequest<ActorModel>
    {
        /// <summary>
        /// Id actor
        /// </summary>
        public int Id { get; set; }
    }
}
