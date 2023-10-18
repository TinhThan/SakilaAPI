using MediatR;
using SakilaAPI.Core.Models.User;

namespace SakilaAPI.Core.CQRS.User.Command
{
    public class LoginCommand : IRequest<AccessTokenResponse>
    {
        public LoginModel LoginModel { get; set; }
    }
}
