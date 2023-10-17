using MediatR;
using SakilaAPI.Core.Models.User;

namespace SakilaAPI.Core.CQRS.User.Command
{
    public class RegisterCommand : IRequest<string>
    {
        public RegisterModel RegisterModel { get; set; }
    }
}
