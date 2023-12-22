using FluentValidation;
using Driving_School.Api.ModelsRequest.Course;

namespace Driving_School.Api.Validators.Course
{
    /// <summary>
    /// Валидатор класса <see cref="CreateCourseRequest"/>
    /// </summary>
    public class CreateCourseRequestValidator : AbstractValidator<CreateCourseRequest>
    {
        /// <summary>
        /// Инициализирую <see cref="CreateCourseRequestValidator"/>
        /// </summary>
        public CreateCourseRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Наименование курса не должно быть пустым или null")
                .MaximumLength(50)
                .WithMessage("Наименование курса больше 50 символов");

            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Описание курса не должен быть пустым или null")
                .MaximumLength(150)
                .WithMessage("Описание курса больше 150 символов");

            RuleFor(x => x.Duration)
                .NotNull()
                .NotEmpty()
                .WithMessage("Продолжительность занятия не должна быть пустой или null");

            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("Цена занятия не должна быть пустой или null");
        }
    }
}
