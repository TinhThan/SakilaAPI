using MediatR;
using SakilaAPI.Core.Models.Actor;

namespace SakilaAPI.Core.CQRS.Actor.Command
{
    public class CapNhatActorQuery : IRequest<string>
    { 
        public int Id { get; set; }
        public ActorCapNhatModel CapNhatModel { get; set; }
    }
}
