using Driving_School.Api.Models;
using Driving_School.Api.Tests.Infrastructures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Driving_School.Api.Tests.IntegrationTests
{
    public class CourseIntegrationTest : BaseClassForIntegrationTests
    {
        public CourseIntegrationTest(Driving_SchoolApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Тест на получение курса по ID (GetById)
        /// </summary>
        [Fact]
        public async Task GetIdShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var course1 = TestDataGenerator.Course();
            var course2 = TestDataGenerator.Course();

            await context.Courses.AddRangeAsync(course1, course2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Course/{course2.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CourseResponse>(resultString);

            result.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    course2.Id,
                    course2.Name,
                    course2.Description,
                    course2.Duration,
                    course2.Price
                });
        }

        /// <summary>
        /// Тест на получения всех курсов (GetAll)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var course1 = TestDataGenerator.Course();
            var course2 = TestDataGenerator.Course(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Courses.AddRangeAsync(course1, course2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Course");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<CourseResponse>>(resultString);

            result.Should().NotBeNull()
                .And
                .Contain(x => x.Id == course1.Id)
                .And
                .NotContain(x => x.Id == course2.Id);
        }

        /// <summary>
        /// Тест на добавление курса (Add)
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var course = TestDataGenerator.CreateCourseRequest();

            // Act
            string data = JsonConvert.SerializeObject(course);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Course", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CourseResponse>(resultString);

            var placeFirst = await context.Courses.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            placeFirst.Should()
                .BeEquivalentTo(course);
        }

        /// <summary>
        /// Тест на изменение курса по ID (Edit)
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var course = TestDataGenerator.Course();
            await context.Courses.AddAsync(course);
            await unitOfWork.SaveChangesAsync();

            var courseRequest = TestDataGenerator.CourseRequest(x => x.Id = course.Id);

            // Act
            string data = JsonConvert.SerializeObject(courseRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Course", contextdata);

            var courseFirst = await context.Courses.FirstAsync(x => x.Id == course.Id);

            // Assert           
            courseFirst.Should()
                .BeEquivalentTo(courseRequest);
        }

        /// <summary>
        /// Тест на удаление курса по ID (Delete)
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var course = TestDataGenerator.Course();
            await context.Courses.AddAsync(course);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Course/{course.Id}");

            var courseFirst = await context.Courses.FirstAsync(x => x.Id == course.Id);

            // Assert
            courseFirst.DeletedAt.Should()
                .NotBeNull();

            course.Should()
                .BeEquivalentTo(new
                {
                    course.Name,
                    course.Description,
                    course.Duration,
                    course.Price
                });
        }
    }
}
