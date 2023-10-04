using FluentValidation;
using SakilaAPI.Core.CQRS.Actor.Query;

namespace SakilaAPI.Validations.Actor
{
    /// <summary>
    /// Validator of ActorDetailQuery
    /// </summary>
    public class ActorDetailQueryValidator : AbstractValidator<ActorDetailQuery>
    {
        /// <summary>
        /// Contructor
        /// </summary>
        public ActorDetailQueryValidator()
        {
            RuleFor(t => t.Id)
                .GreaterThan(0).WithMessage("Id_Not_Empty");
        }
    }
}
