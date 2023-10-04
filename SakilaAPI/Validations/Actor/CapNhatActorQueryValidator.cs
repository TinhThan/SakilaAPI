using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SakilaAPI.Core;
using SakilaAPI.Core.Contants;
using SakilaAPI.Core.CQRS.Actor.Command;
using SakilaAPI.Core.CQRS.Actor.CommandHandler;

namespace SakilaAPI.Validations.Actor
{
    public class CapNhatActorQueryValidator : AbstractValidator<CapNhatActorQuery>
    {
        private readonly DataContext _dataContext;

        public CapNhatActorQueryValidator(DataContext dataContext)
        {
            _dataContext = dataContext;

            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage("Id_Not_Empty")
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await _dataContext.Actors.AnyAsync(t => t.Id == id, cancellationToken);
                }).WithMessage("Actor_Not_Exists");

            RuleFor(t => t.CapNhatModel.FirstName)
                .NotNull().WithMessage("First_Name_Not_Empty")
                .NotEmpty().WithMessage("First_Name_Not_Empty");

            RuleFor(t => t.CapNhatModel.LastName)
                .NotNull().WithMessage("Last_Name_Not_Empty")
                .NotEmpty().WithMessage("Last_Name_Not_Empty");
        }
    }
}
