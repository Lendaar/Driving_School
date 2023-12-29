using Driving_School.Api.Models;
using Driving_School.Api.Tests.Infrastructures;
using Driving_School.Context.Contracts.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Text;
using Xunit;

namespace Driving_School.Api.Tests.IntegrationTests
{
    public class LessonIntegrationTest : BaseClassForIntegrationTests
    {
        private readonly Person person1;
        private readonly Person person2;
        private readonly Employee employee1;
        private readonly Employee employee2;
        private readonly Place place;
        private readonly Transport transport;
        private readonly Course course;

        public LessonIntegrationTest(Driving_SchoolApiFixture fixture) : base(fixture)
        {
            person1 = TestDataGenerator.Person();
            person2 = TestDataGenerator.Person();
            context.Persons.AddRangeAsync(person1, person2);

            employee1 = TestDataGenerator.Employee();
            employee2 = TestDataGenerator.Employee();
            employee1.PersonId = person1.Id;
            employee2.PersonId = person2.Id;
            employee2.EmployeeType = Context.Contracts.Enums.EmployeeTypes.Instructor;
            context.Employees.AddRangeAsync(employee1, employee2);

            place = TestDataGenerator.Place();
            context.Places.AddAsync(place);

            transport = TestDataGenerator.Transport();
            context.Transports.AddAsync(transport);

            course = TestDataGenerator.Course();
            context.Courses.AddAsync(course);

            unitOfWork.SaveChangesAsync().Wait();
        }

        /// <summary>
        /// Тест на получение занятия по ID (GetById)
        /// </summary>
        [Fact]
        public async Task GetIdShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var lesson2 = TestDataGenerator.Lesson();

            lesson2.CourceId = course.Id;
            lesson2.StudentId = employee1.Id;
            lesson2.InstructorId = employee2.Id;
            lesson2.TransportId = transport.Id;
            lesson2.PlaceId = place.Id;

            await context.Lessons.AddAsync(lesson2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Lesson/{lesson2.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LessonResponse>(resultString);

            result.CourseName.Should().NotBeNull()
                .And
                .BeEquivalentTo(course.Name);
            result.PlaceName.Should().NotBeNull()
                .And
                .BeEquivalentTo(place.Name);
            result.TransportName.Should().NotBeNull()
                .And
                .BeEquivalentTo(transport.Name);
            result.InstructorName.Should().NotBeNull()
                .And
                .BeEquivalentTo($"{person2.LastName} {person2.FirstName} {person2.Patronymic}");
            result.StudentName.Should().NotBeNull()
                .And
                .BeEquivalentTo($"{person1.LastName} {person1.FirstName} {person1.Patronymic}");
        }

        /// <summary>
        /// Тест на получения всех занятий (GetAll)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var lesson2 = TestDataGenerator.Lesson(x => x.DeletedAt = DateTimeOffset.Now);

            lesson2.CourceId = course.Id;
            lesson2.StudentId = employee1.Id;
            lesson2.InstructorId = employee2.Id;
            lesson2.TransportId = transport.Id;
            lesson2.PlaceId = place.Id;

            await context.Lessons.AddAsync(lesson2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Lesson");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<LessonResponse>>(resultString);

            result.Should().BeNullOrEmpty();
        }

        /// <summary>
        /// Тест на добавление занятия (Add)
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var lesson = TestDataGenerator.CreateLessonRequest();

            lesson.Course = course.Id;
            lesson.Student = employee1.Id;
            lesson.Instructor = employee2.Id;
            lesson.Transport = transport.Id;
            lesson.Place = place.Id;

            // Act
            string data = JsonConvert.SerializeObject(lesson);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Lesson", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LessonResponse>(resultString);

            var lessonFirst = await context.Lessons.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            lessonFirst.CourceId.Should().Be(course.Id);
            lessonFirst.InstructorId.Should().Be(employee2.Id);
            lessonFirst.StudentId.Should().Be(employee1.Id);
            lessonFirst.TransportId.Should().Be(transport.Id);
            lessonFirst.PlaceId.Should().Be(place.Id);
        }

        /// <summary>
        /// Тест на изменение занятия по ID (Edit)
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var lesson = TestDataGenerator.Lesson();

            lesson.CourceId = course.Id;
            lesson.StudentId = employee1.Id;
            lesson.InstructorId = employee2.Id;
            lesson.TransportId = transport.Id;
            lesson.PlaceId = place.Id;

            await context.Lessons.AddAsync(lesson);
            await unitOfWork.SaveChangesAsync();

            var lessonRequest = TestDataGenerator.LessonRequest(x => x.Id = lesson.Id);

            // Act
            string data = JsonConvert.SerializeObject(lessonRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Lesson", contextdata);

            var personFirst = await context.Lessons.FirstAsync(x => x.Id == lesson.Id);

            // Assert           
            personFirst.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    lesson.Id,
                    lesson.StudentId,
                    lesson.InstructorId,
                    lesson.PlaceId,
                    lesson.CourceId,
                    lesson.TransportId
                });
        }

        /// <summary>
        /// Тест на удаление занятия по ID (Delete)
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var lesson = TestDataGenerator.Lesson();

            lesson.CourceId = course.Id;
            lesson.StudentId = employee1.Id;
            lesson.InstructorId = employee2.Id;
            lesson.TransportId = transport.Id;
            lesson.PlaceId = place.Id;

            await context.Lessons.AddAsync(lesson);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Lesson/{lesson.Id}");

            var lessonFirst = await context.Lessons.FirstAsync(x => x.Id == lesson.Id);

            // Assert
            lessonFirst.DeletedAt.Should()
                .NotBeNull();

            lessonFirst.Should()
                .BeEquivalentTo(new
                {
                    lesson.StartDate,
                    lesson.EndDate
                });
        }
    }
}
