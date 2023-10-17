using AutoMapper;
using MediatR;
using SakilaAPI.Core.Contants;
using SakilaAPI.Core.CQRS.User.Command;
using SakilaAPI.Core.Entities;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Middlewares;

namespace SakilaAPI.Core.CQRS.User.CommandHandler
{
    public class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommand, string>
    {
        public RegisterCommandHandler(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var newUser = _mapper.Map<UserEntity>(request.RegisterModel);
            newUser.Password = HelperIdentity.HashPasswordSalt(request.RegisterModel.Password);
            newUser.LastUpdate = DateTime.UtcNow;

            try
            {
                await _dataContext.AddAsync(newUser, cancellationToken);
                var resultCreate = await _dataContext.SaveChangesAsync(cancellationToken);
                if (resultCreate > 0)
                {
                    return MessageSystem.ADD_SUCCESS;
                }
                throw new StatusServerErrorException(MessageSystem.ADD_FAIL);
            }
            catch (Exception ex)
            {
                throw new StatusServerErrorException(MessageSystem.ADD_FAIL, ex.Message);
            }
        }
    }
}
