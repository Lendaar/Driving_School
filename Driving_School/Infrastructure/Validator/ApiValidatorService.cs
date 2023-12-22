using FluentValidation;
using Driving_School.Api.Validators.Transport;
using Driving_School.Api.Validators.Course;
using Driving_School.Api.Validators.Employee;
using Driving_School.Api.Validators.Lesson;
using Driving_School.Api.Validators.Person;
using Driving_School.Api.Validators.Place;
using Driving_School.Services.Contracts.Exceptions;
using Driving_School.Shared;
using Driving_School.Repositories.Contracts.Interface;

namespace Driving_School.Api.Infrastructures.Validator
{
    internal sealed class ApiValidatorService : IApiValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ApiValidatorService(
            IPersonReadRepository personReadRepository,
            IEmployeeReadRepository employeeReadRepository,
            ITransportReadRepository transportReadRepository,
            ICourseReadRepository courseReadRepository,
            IPlaceReadRepository placeReadRepository)
        {
            Register<CreateCourseRequestValidator>();
            Register<CourseRequestValidator>();

            Register<CreateTransportRequestValidator>();
            Register<TransportRequestValidator>();

            Register<CreatePlaceRequestValidator>();
            Register<PlaceRequestValidator>();

            Register<CreatePersonRequestValidator>();
            Register<PersonRequestValidator>();

            Register<CreateEmployeeRequestValidator>(personReadRepository);
            Register<EmployeeRequestValidator>(personReadRepository);

            Register<CreateLessonRequestValidator>(employeeReadRepository, placeReadRepository, transportReadRepository, courseReadRepository);
            Register<LessonRequestValidator>(employeeReadRepository, placeReadRepository, transportReadRepository, courseReadRepository);
        }

        ///<summary>
        /// Регистрирует валидатор в словаре
        /// </summary>
        public void Register<TValidator>(params object[] constructorParams)
            where TValidator : IValidator
        {
            var validatorType = typeof(TValidator);
            var innerType = validatorType.BaseType?.GetGenericArguments()[0];
            if (innerType == null)
            {
                throw new ArgumentNullException($"Указанный валидатор {validatorType} должен быть generic от типа IValidator");
            }

            if (constructorParams?.Any() == true)
            {
                var validatorObject = Activator.CreateInstance(validatorType, constructorParams);
                if (validatorObject is IValidator validator)
                {
                    validators.TryAdd(innerType, validator);
                }
            }
            else
            {
                validators.TryAdd(innerType, Activator.CreateInstance<TValidator>());
            }
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new Driving_SchoolValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
