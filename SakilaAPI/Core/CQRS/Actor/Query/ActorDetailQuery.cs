using MediatR;
using SakilaAPI.Core.Models.Actor;

namespace SakilaAPI.Core.CQRS.Actor.Query
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
