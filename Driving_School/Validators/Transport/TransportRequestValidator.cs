using Driving_School.Api.ModelsRequest.Transport;
using FluentValidation;

namespace Driving_School.Api.Validators.Transport
{
    /// <summary>
    /// 
    /// </summary>
    public class TransportRequestValidator : AbstractValidator<TransportRequest>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TransportRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id не должно быть пустым или null");

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
