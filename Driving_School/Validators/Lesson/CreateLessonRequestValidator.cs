using Driving_School.Api.ModelsRequest.Lesson;
using Driving_School.Context.Contracts.Enums;
using Driving_School.Repositories.Contracts.Interface;
using FluentValidation;

namespace Driving_School.Api.Validators.Lesson
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateLessonRequestValidator : AbstractValidator<CreateLessonRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public CreateLessonRequestValidator(
            IEmployeeReadRepository employeeReadRepository,
            IPlaceReadRepository placeReadRepository,
            ITransportReadRepository transportReadRepository,
            ICourseReadRepository courseReadRepository)
        {

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
                    var place = await placeReadRepository.GetByIdAsync(id, CancellationToken);
                    return place != null;
                })
                .WithMessage("Такой площадки не существует!");

            RuleFor(x => x.Transport)
                .NotNull()
                .NotEmpty()
                .WithMessage("Транспорт не должен быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var transport = await transportReadRepository.GetByIdAsync(id, CancellationToken);
                    return transport != null;
                })
                .WithMessage("Такого транспорта не существует!");

            RuleFor(x => x.Course)
                .NotNull()
                .NotEmpty()
                .WithMessage("Курс не должен быть пустым или null")
                .MustAsync(async (id, CancellationToken) =>
                {
                    var course = await courseReadRepository.GetByIdAsync(id, CancellationToken);
                    return course != null;
                })
                .WithMessage("Такого курса не существует!");

            RuleFor(x => x.Instructor)
               .NotNull()
               .NotEmpty()
               .WithMessage("Инструктор не должен быть пустым или null")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var instructor = await employeeReadRepository.GetByIdAsync(id, CancellationToken);
                   return instructor != null;
               })
               .WithMessage("Такого инструктора не существует!")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var instructor = await employeeReadRepository.GetByIdAsync(id, CancellationToken);
                   if (instructor == null)
                   {
                       return false;
                   }
                   return instructor!.EmployeeType == EmployeeTypes.Instructor;
               })
                .WithMessage("Работник не соответствует категории: Инструктор!");

            RuleFor(x => x.Student)
               .NotNull()
               .NotEmpty()
               .WithMessage("Студент не должен быть пустым или null")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var student = await employeeReadRepository.GetByIdAsync(id, CancellationToken);
                   return student != null;
               })
               .WithMessage("Такого студента не существует!")
               .MustAsync(async (id, CancellationToken) =>
               {
                   var student = await employeeReadRepository.GetByIdAsync(id, CancellationToken);
                   if (student == null)
                   {
                       return false;
                   }
                   return student!.EmployeeType == EmployeeTypes.Student;
               })
                .WithMessage("Работник не соответствует категории: Студент!");
        }

    }

}
