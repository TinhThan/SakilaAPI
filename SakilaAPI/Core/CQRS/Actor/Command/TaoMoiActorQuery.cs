using MediatR;
using SakilaAPI.Core.Models;
using SakilaAPI.Core.Models.Actor;

namespace SakilaAPI.Core.CQRS.Actor.Command
{
    public class TaoMoiActorQuery : IRequest<string>
    {
        public ActorTaoMoiModel ActorTaoMoiModel { get; set; }
    }
}
