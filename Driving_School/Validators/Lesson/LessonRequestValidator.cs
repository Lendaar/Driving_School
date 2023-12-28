using Driving_School.Api.ModelsRequest.Lesson;
using Driving_School.Context.Contracts.Enums;
using Driving_School.Repositories.Contracts.Interface;
using FluentValidation;

namespace Driving_School.Api.Validators.Lesson
{
    /// <summary>
    /// 
    /// </summary>
    public class LessonRequestValidator : AbstractValidator<LessonRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public LessonRequestValidator(
            IEmployeeReadRepository employeeReadRepository,
            IPlaceReadRepository placeReadRepository,
            ITransportReadRepository transportReadRepository,
            ICourseReadRepository courseReadRepository)
        {
            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty()
              .WithMessage("Id не должен быть пустым или null");

            RuleFor(x => x.StartDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("Начало занятия не должно быть пустым или null");

            RuleFor(x => x.EndDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("Конец занятия не должно быть пустым или null");

            RuleFor(x => x.Place)
                .NotNull()
                .NotEmpty()
                .WithMessage("Площадка не должна быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var placeExists = await placeReadRepository.AnyByIdAsync(id, CancellationToken);
                    return placeExists;
                })
                .WithMessage("Такой площадки не существует!");

            RuleFor(x => x.Transport)
                .NotNull()
                .NotEmpty()
                .WithMessage("Транспорт не должен быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var transportExists = await transportReadRepository.AnyByIdAsync(id, CancellationToken);
                    return transportExists;
                })
                .WithMessage("Такого транспорта не существует!");

            RuleFor(x => x.Course)
                .NotNull()
                .NotEmpty()
                .WithMessage("Курс не должен быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var courseExists = await courseReadRepository.AnyByIdAsync(id, CancellationToken);
                    return courseExists;
                })
                .WithMessage("Такого курса не существует!");


            RuleFor(x => x.Instructor)
               .NotNull()
               .NotEmpty()
               .WithMessage("Инструктор не должен быть пустым или null")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var instructorExists = await employeeReadRepository.AnyByIdAsync(id, CancellationToken);
                   return instructorExists;
               })
               .WithMessage("Такого инструктора не существует!")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var employeeExistsWithInstructor = await employeeReadRepository.AnyByIdWithInstructorAsync(id, CancellationToken);
                   return employeeExistsWithInstructor;
               })
                .WithMessage("Работник не соответствует категории: Инструктор!");

            RuleFor(x => x.Student)
               .NotNull()
               .NotEmpty()
               .WithMessage("Студент не должен быть пустым или null")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var studentExists = await employeeReadRepository.AnyByIdAsync(id, CancellationToken);
                   return studentExists;
               })
               .WithMessage("Такого студента не существует!")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var employeeExistsWithStudent = await employeeReadRepository.AnyByIdWithStudentAsync(id, CancellationToken);
                   return employeeExistsWithStudent;
               })
                .WithMessage("Работник не соответствует категории: Студент!");
        }

    }
}
