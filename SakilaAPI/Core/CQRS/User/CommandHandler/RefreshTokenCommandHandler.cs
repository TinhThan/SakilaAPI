using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core.Contants;
using SakilaAPI.Core.CQRS.User.Command;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Middlewares;

namespace SakilaAPI.Core.CQRS.User.CommandHandler
{
    public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommand, string>
    {
        public RefreshTokenCommandHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pricipal = HelperIdentity.GetPrincipalFromExpiredToken(request.RefreshTokenModel.AccessToken);
                var userName = pricipal.Identity.Name;

                var userExists = await _dataContext.Users.FirstOrDefaultAsync(t => t.UserName == userName, cancellationToken);

                if (userExists == null)
                {
                    throw new StatusSuccessException(StatusCodes.Status204NoContent, "Người dùng không tồn tại", userName);
                }

                if (userExists.RefreshToken != request.RefreshTokenModel.RefreshToken)
                {
                    throw new StatusSuccessException(StatusCodes.Status401Unauthorized, MessageSystem.REFRESH_TOKEN_INVALID, userName);
                }

                if (userExists.RefreshTokenExpiryTime < DateTime.UtcNow)
                {
                    throw new StatusSuccessException(StatusCodes.Status401Unauthorized, MessageSystem.REFRESH_TOKEN_EXPIRED, userName);
                }

                var newAccessToken = HelperIdentity.GenerateToken(pricipal.Claims.ToList());
                return newAccessToken;
            }
            catch (Exception e)
            {
                if (e is CustomException)
                {
                    throw;
                }
                throw new StatusSuccessException(StatusCodes.Status401Unauthorized, MessageSystem.AUTH_AUTHENTICATED_ERROR, e.Message);
            }
        }
    }
}
