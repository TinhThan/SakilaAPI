using MediatR;
using Sakila_B.Core.Models;
using Sakila_B.Core.Models.Actor;

namespace Sakila_B.Core.CQRS.Actor.Command
{
    public class TaoMoiActorQuery : IRequest<string>
    {
        public ActorTaoMoiModel ActorTaoMoiModel { get; set; }
    }
}
