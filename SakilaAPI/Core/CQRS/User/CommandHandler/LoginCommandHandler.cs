using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.Contants;
using SakilaAPI.Core.CQRS.User.Command;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Middlewares;

namespace SakilaAPI.Core.CQRS.User.CommandHandler
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommand, Dictionary<string, string>>
    {
        public LoginCommandHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<Dictionary<string, string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var password = HelperIdentity.HashPasswordSalt(request.LoginModel.Password);
            var userExists = await _dataContext.Users.FirstOrDefaultAsync(t => t.UserName == request.LoginModel.UserName
                                                && t.Password == password, cancellationToken);
            if (userExists == null)
            {
                throw new StatusSuccessException(StatusCodes.Status204NoContent, "Người dùng không tồn tại", request.LoginModel.UserName);
            }

            var claims = new Dictionary<string, string>();
            claims.Add("username", userExists.UserName);
            claims.Add("permission", userExists.Permission);

            var refreshToken = userExists.RefreshToken = HelperIdentity.GenerateRefreshToken();
            try
            {
                _dataContext.Users.Update(userExists);
                var resultUpdate = await _dataContext.SaveChangesAsync(cancellationToken);
                if (resultUpdate > 0)
                {
                    return new Dictionary<string, string>() { { "AccessToken",HelperIdentity.GenerateToken(claims)},
                        {"RefreshToken",refreshToken }};
                }
                throw new StatusServerErrorException(MessageSystem.LOGIN_FAIL);
            }
            catch (Exception ex)
            {
                throw new StatusServerErrorException(MessageSystem.LOGIN_FAIL, ex.Message);
            }
        }
    }
}
