using FluentValidation;
using SakilaAPI.Core;
using SakilaAPI.Core.CQRS.Actor.Command;
using SakilaAPI.Core.CQRS.Actor.CommandHandler;

namespace SakilaAPI.Validations.Actor
{
    public class TaoMoiActorQueryValidator : AbstractValidator<TaoMoiActorQuery>
    {
        public TaoMoiActorQueryValidator()
        {

            RuleFor(t => t.ActorTaoMoiModel.FirstName)
                .NotNull().WithMessage("First_Name_Not_Empty")
                .NotEmpty().WithMessage("First_Name_Not_Empty");

            RuleFor(t => t.ActorTaoMoiModel.LastName)
                .NotNull().WithMessage("Last_Name_Not_Empty")
                .NotEmpty().WithMessage("Last_Name_Not_Empty");
        }
    }
}
