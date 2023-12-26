using Driving_School.Context.Contracts.Models;
using Driving_School.Services.Contracts.RequestModels;
using GSBTypesContext = Driving_School.Context.Contracts.Enums.GSBTypes;
using GSBTypesServices = Driving_School.Services.Contracts.Enums.GSBTypes;
using EmployeeTypesContext = Driving_School.Context.Contracts.Enums.EmployeeTypes;
using EmployeeTypesServices = Driving_School.Services.Contracts.Enums.EmployeeTypes;

namespace Driving_School.Services.Tests
{
    static internal class TestDataGenerator
    {
        static internal Course Course(Action<Course>? action = null)
        {
            var result = new Course
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Description = $"Description{Guid.NewGuid():N}",
                Duration = Random.Shared.Next(20, 60),
                Price = Random.Shared.Next(1000, 5000),
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(result);
            return result;
        }

        static internal CourseRequestModel CourseRequestModel(Action<CourseRequestModel>? action = null)
        {
            var result = new CourseRequestModel
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Description = $"Description{Guid.NewGuid():N}",
                Duration = Random.Shared.Next(20, 60),
                Price = Random.Shared.Next(1000, 5000),
            };

            action?.Invoke(result);
            return result;
        }

        static internal Transport Transport(Action<Transport>? action = null)
        {
            var item = new Transport
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Number = $"Number{Guid.NewGuid():N}",
                GSBType = GSBTypesContext.Mechanical,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal TransportRequestModel TransportRequestModel(Action<TransportRequestModel>? action = null)
        {
            var item = new TransportRequestModel
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Number = $"Number{Guid.NewGuid():N}",
                GSBType = GSBTypesServices.Mechanical,
            };

            action?.Invoke(item);
            return item;
        }

        static internal Person Person(Action<Person>? action = null)
        {
            var item = new Person
            {
                Id = Guid.NewGuid(),
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
                Patronymic = $"Patronymic{Guid.NewGuid():N}",
                DateOfBirthday = DateTime.UtcNow.AddYears(-20),
                Passport = $"Passport{Guid.NewGuid():N}",
                Phone = $"Phone{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal PersonRequestModel PersonRequestModel(Action<PersonRequestModel>? action = null)
        {
            var item = new PersonRequestModel
            {
                Id = Guid.NewGuid(),
                LastName = $"LastName{Guid.NewGuid():N}",
                FirstName = $"FirstName{Guid.NewGuid():N}",
                Patronymic = $"Patronymic{Guid.NewGuid():N}",
                DateOfBirthday = DateTime.UtcNow.AddYears(-20),
                Passport = $"Passport{Guid.NewGuid():N}",
                Phone = $"Phone{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal Employee Employee(Action<Employee>? action = null)
        {
            var item = new Employee
            {
                Id = Guid.NewGuid(),
                EmployeeType = EmployeeTypesContext.Student,
                Email = $"Email{Guid.NewGuid():N}",
                Experience = 0,
                Number = $"Number{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal EmployeeRequestModel EmployeeRequestModel(Action<EmployeeRequestModel>? action = null)
        {
            var item = new EmployeeRequestModel
            {
                Id = Guid.NewGuid(),
                EmployeeType = EmployeeTypesServices.Student,
                Email = $"Email{Guid.NewGuid():N}",
                Experience = 0,
                Number = $"Number{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal Place Place(Action<Place>? action = null)
        {
            var item = new Place
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Description = $"Description{Guid.NewGuid():N}",
                Address = $"Address{Guid.NewGuid():N}",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal PlaceRequestModel PlaceRequestModel(Action<PlaceRequestModel>? action = null)
        {
            var item = new PlaceRequestModel
            {
                Id = Guid.NewGuid(),
                Name = $"Name{Guid.NewGuid():N}",
                Description = $"Description{Guid.NewGuid():N}",
                Address = $"Address{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal Lesson Lesson(Action<Lesson>? action = null)
        {
            var item = new Lesson
            {
                Id = Guid.NewGuid(),
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            action?.Invoke(item);
            return item;
        }

        static internal LessonRequestModel LessonRequestModel(Action<LessonRequestModel>? action = null)
        {
            var item = new LessonRequestModel
            {
                Id = Guid.NewGuid(),
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow.AddHours(2),
            };

            action?.Invoke(item);
            return item;
        }
    }
}