using Driving_School.Api.ModelsRequest.Place;
using FluentValidation;

namespace Driving_School.Api.Validators.Place
{
    /// <summary>
    /// 
    /// </summary>
    public class PlaceRequestValidator : AbstractValidator<PlaceRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public PlaceRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.Name)
               .NotNull()
               .NotEmpty()
               .WithMessage("Наименование площадки не должно быть пустым или null")
               .MaximumLength(50)
               .WithMessage("Наименование площадки больше 50 символов");

            RuleFor(x => x.Address)
               .NotNull()
               .NotEmpty()
               .WithMessage("Адрес площадки не должен быть пустым или null")
               .MaximumLength(100)
               .WithMessage("Адрес площадки больше 100 символов");
        }
    }
}
