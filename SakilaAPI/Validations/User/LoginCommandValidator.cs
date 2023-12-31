﻿using FluentValidation;
using SakilaAPI.Core.Contants;
using SakilaAPI.Core.CQRS.User.Command;

namespace SakilaAPI.Validations.User
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(v => v.LoginModel.UserName)
                .NotEmpty().WithMessage(MessageSystem.USERNAME_IS_EMPTY);

            RuleFor(v => v.LoginModel.Password)
                .NotEmpty().WithMessage(MessageSystem.PASSWORD_IS_EMPTY);
        }
    }
}
