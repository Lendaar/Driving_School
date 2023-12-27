using AutoMapper;
using FluentAssertions;
using Driving_School.Context.Contracts.Enums;
using Driving_School.Context.Tests;
using Driving_School.Repositories.Implementations;
using Driving_School.Services.Automappers;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Implementations;
using Xunit;
using Driving_School.Services.Contracts.Exceptions;
using Driving_School.Context.Contracts.Models;

namespace Driving_School.Services.Tests.Tests
{
    public class LessonServiceTests : Driving_SchoolContextInMemory
    {
        private readonly ILessonService lessonService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="LessonServiceTests"/>
        /// </summary>

        public LessonServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            lessonService = new LessonService(
                new LessonReadRepositories(Reader),
                new CourseReadRepository(Reader),
                new TransportReadRepository(Reader),
                new EmployeeReadRepository(Reader),
                new PlaceReadRepository(Reader),
                new LessonWriteRepository(WriterContext),
                UnitOfWork,
                config.CreateMapper()
            );
        }

        /// <summary>
        /// Получение занятия по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => lessonService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<Driving_SchoolEntityNotFoundException<Lesson>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение занятия по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Lesson();
            await Context.Lessons.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await lessonService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.StartDate,
                    target.EndDate
                });
        }

        // <summary>
        /// Добавление занятия, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.LessonRequestModel();

            var transport = TestDataGenerator.Transport();
            await Context.Transports.AddAsync(transport);

            var place = TestDataGenerator.Place();
            await Context.Places.AddAsync(place);

            var course = TestDataGenerator.Course();
            await Context.Courses.AddAsync(course);

            var person1 = TestDataGenerator.Person();
            await Context.Persons.AddAsync(person1);

            var person2 = TestDataGenerator.Person();
            await Context.Persons.AddAsync(person2);

            var instructor = TestDataGenerator.Employee();
            instructor.PersonId = person1.Id;
            instructor.EmployeeType = EmployeeTypes.Instructor;
            await Context.Employees.AddAsync(instructor);

            var student = TestDataGenerator.Employee();
            student.PersonId = person2.Id;
            student.EmployeeType = EmployeeTypes.Student;
            await Context.Employees.AddAsync(student);

            target.Transport = transport.Id;
            target.Place = place.Id;
            target.Course = course.Id;
            target.Instructor = instructor.Id;
            target.Student = student.Id;

            //Act
            var act = await lessonService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Lessons.Single(x =>
                x.Id == act.Id &&
                x.TransportId == target.Transport &&
                x.PlaceId == target.Place &&
                x.CourceId == target.Course &&
                x.InstructorId == target.Instructor &&
                x.StudentId == target.Student
            );
            entity.Should().NotBeNull();

            instructor.EmployeeType.Should().Be(EmployeeTypes.Instructor);
            student.EmployeeType.Should().Be(EmployeeTypes.Student);
        }

        /// <summary>
        /// Изменение занятия, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Lesson();

            var transport = TestDataGenerator.Transport();
            var transportModel = TestDataGenerator.Transport();
            await Context.Transports.AddRangeAsync(transport, transportModel);

            var place = TestDataGenerator.Place();
            var placeModel = TestDataGenerator.Place();
            await Context.Places.AddRangeAsync(place, placeModel);

            var course = TestDataGenerator.Course();
            var courseModel = TestDataGenerator.Course();
            await Context.Courses.AddRangeAsync(course, courseModel);

            var person1 = TestDataGenerator.Person();
            var personModel1 = TestDataGenerator.Person();
            await Context.Persons.AddRangeAsync(person1, personModel1);

            var person2 = TestDataGenerator.Person();
            var personModel2 = TestDataGenerator.Person();
            await Context.Persons.AddRangeAsync(person2, personModel2);

            var instructor = TestDataGenerator.Employee();
            instructor.PersonId = person1.Id;
            instructor.EmployeeType = EmployeeTypes.Instructor;
            var instructorModel = TestDataGenerator.Employee();
            instructorModel.PersonId = personModel1.Id;
            instructorModel.EmployeeType = EmployeeTypes.Instructor;
            await Context.Employees.AddRangeAsync(instructor, instructorModel);

            var student = TestDataGenerator.Employee();
            student.PersonId = person2.Id;
            student.EmployeeType = EmployeeTypes.Student;
            var studentModel = TestDataGenerator.Employee();
            studentModel.PersonId = personModel2.Id;
            studentModel.EmployeeType = EmployeeTypes.Student;
            await Context.Employees.AddRangeAsync(student, studentModel);

            target.TransportId = transport.Id;
            target.PlaceId = place.Id;
            target.CourceId = course.Id;
            target.InstructorId = instructor.Id;
            target.StudentId = student.Id;

            await Context.Lessons.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.LessonRequestModel();
            targetModel.Id = target.Id;
            targetModel.Transport = transportModel.Id;
            targetModel.Place = placeModel.Id;
            targetModel.Course = courseModel.Id;
            targetModel.Instructor = instructorModel.Id;
            targetModel.Student = studentModel.Id;

            //Act
            var act = await lessonService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Lessons.Single(x =>
                x.Id == act.Id &&
                x.TransportId == targetModel.Transport &&
                x.PlaceId == targetModel.Place &&
                x.CourceId == targetModel.Course &&
                x.InstructorId == targetModel.Instructor &&
                x.StudentId == targetModel.Student
            );
            entity.Should().NotBeNull();

            instructorModel.EmployeeType.Should().Be(EmployeeTypes.Instructor);
            studentModel.EmployeeType.Should().Be(EmployeeTypes.Student);
        }

        /// <summary>
        /// Удаление занятия, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Lesson();
            await Context.Lessons.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => lessonService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Lessons.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}

