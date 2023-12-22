using Driving_School.Api.ModelsRequest.Person;
using FluentValidation;

namespace Driving_School.Api.Validators.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePersonRequestValidator : AbstractValidator<CreatePersonRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreatePersonRequestValidator()
        {
            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Фамилия не должна быть пустой или null")
                .MaximumLength(80)
                .WithMessage("Фамилия больше 80 символов");

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым или null")
                .MaximumLength(80)
                .WithMessage("Имя больше 80 символов");

            RuleFor(x => x.DateOfBirthday)
                .NotNull()
                .NotEmpty()
                .WithMessage("дата рождения не должна быть пустой или null");

            RuleFor(x => x.Passport)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер паспорта не должен быть пустой или null");

            RuleFor(x => x.Phone)
                .NotNull()
                .NotEmpty()
                .WithMessage("Телефон не должен быть пустым или null");
        }
    }
}
