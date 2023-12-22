using Driving_School.Api.ModelsRequest.Employee;
using Driving_School.Repositories.Contracts.Interface;
using FluentValidation;

namespace Driving_School.Api.Validators.Employee
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public EmployeeRequestValidator(IPersonReadRepository personReadRepository)
        {
            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.EmployeeType)
                .NotNull()
                .WithMessage("Тип работника не должен быть null");

            RuleFor(x => x.Email)
               .NotNull()
               .NotEmpty()
               .WithMessage("Почта не должна быть пустой или null")
               .EmailAddress()
               .WithMessage("Требуется действительная почта!");

            RuleFor(x => x.Experience)
                .NotNull()
                .NotEmpty()
                .WithMessage("Стаж не должен быть пустой или null");

            RuleFor(x => x.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("Внутренний номер не должен быть пустым или null")
                .MaximumLength(15)
                .WithMessage("Внутренний номер больше 15 символов");

            RuleFor(x => x.Person)
                .NotNull()
                .NotEmpty()
                .WithMessage("Персона не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var person = await personReadRepository.GetByIdAsync(id, CancellationToken);
                    return person != null;
                })
                .WithMessage("Такой персоны не существует!");
        }
    }
}
