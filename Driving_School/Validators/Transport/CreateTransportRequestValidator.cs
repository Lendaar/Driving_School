using FluentValidation;
using Driving_School.Api.ModelsRequest.Transport;

namespace Driving_School.Api.Validators.Transport
{
    /// <summary>
    /// Валидатор класса <see cref="CreateTransportRequest"/>
    /// </summary>
    public class CreateTransportRequestValidator : AbstractValidator<CreateTransportRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateTransportRequestValidator"/>
        /// </summary>
        public CreateTransportRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Наименование транспорта не должно быть пустым или null")
                .MaximumLength(50)
                .WithMessage("Наименование транспорта больше 50 символов");

            RuleFor(x => x.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("Гос номер не должен быть пустым или null")
                .MaximumLength(10)
                .WithMessage("Гос номер больше 10 символов");

            RuleFor(x => x.GSBType)
                .NotNull()
                .WithMessage("Тип КПП не должен быть null");
        }
    }
}
