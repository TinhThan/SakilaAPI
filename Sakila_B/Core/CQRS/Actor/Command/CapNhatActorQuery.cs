using MediatR;
using Sakila_B.Core.Models.Actor;

namespace Sakila_B.Core.CQRS.Actor.Command
{
    public class CapNhatActorQuery : IRequest<string>
    { 
        public int Id { get; set; }
        public ActorCapNhatModel CapNhatModel { get; set; }
    }
}
