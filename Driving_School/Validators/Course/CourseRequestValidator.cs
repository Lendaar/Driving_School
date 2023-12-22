using Driving_School.Api.ModelsRequest.Course;
using FluentValidation;

namespace Driving_School.Api.Validators.Course
{
    /// <summary>
    /// 
    /// </summary>
    public class CourseRequestValidator : AbstractValidator<CourseRequest>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CourseRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id не должно быть пустым или null");

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
