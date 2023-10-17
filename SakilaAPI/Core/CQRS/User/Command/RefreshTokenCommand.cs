using MediatR;
using SakilaAPI.Core.Models.User;

namespace SakilaAPI.Core.CQRS.User.Command
{
    public class RefreshTokenCommand : IRequest<string>
    {
        public RefreshTokenModel RefreshTokenModel { get; set; }
    }
}
