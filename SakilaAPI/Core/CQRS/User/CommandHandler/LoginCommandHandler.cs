using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.Contants;
using SakilaAPI.Core.CQRS.User.Command;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Middlewares;
using SakilaAPI.Core.Models.User;
using System.Security.Claims;

namespace SakilaAPI.Core.CQRS.User.CommandHandler
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommand, AccessTokenResponse>
    {
        public LoginCommandHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<AccessTokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var password = HelperIdentity.HashPasswordSalt(request.LoginModel.Password);
            var userExists = await _dataContext.Users.FirstOrDefaultAsync(t => t.UserName == request.LoginModel.UserName
                                                && t.Password == password, cancellationToken);
            if (userExists == null)
            {
                throw new StatusSuccessException(StatusCodes.Status204NoContent, "Người dùng không tồn tại", request.LoginModel.UserName);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,userExists.UserName),
                new Claim(ClaimTypes.Role,userExists.Permission)
            };
            var refreshToken = userExists.RefreshToken = HelperIdentity.GenerateRefreshToken();
            userExists.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(CurrentOption.AuthenticationString.ExpiredRefreshToken);
            try
            {
                _dataContext.Users.Update(userExists);
                var resultUpdate = await _dataContext.SaveChangesAsync(cancellationToken);
                if (resultUpdate > 0)
                {
                    return new AccessTokenResponse() { AccessToken = HelperIdentity.GenerateToken(claims),
                        RefreshToken = refreshToken
                    };
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
