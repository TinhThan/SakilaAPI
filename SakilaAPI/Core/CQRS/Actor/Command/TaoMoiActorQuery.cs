using MediatR;
using SakilaAPI.Core.Models;

namespace SakilaAPI.Core.CQRS.Actor.Command
{
    public class TaoMoiActorQuery : IRequest<string>
    {
        public ActorTaoMoiModel ActorTaoMoiModel { get; set; }
    }
}
