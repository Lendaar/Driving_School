using Driving_School.Api.Models;
using Driving_School.Api.Tests.Infrastructures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Driving_School.Api.Tests.IntegrationTests
{
    public class PersonIntegrationTest : BaseClassForIntegrationTests
    {
        public PersonIntegrationTest(Driving_SchoolApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Тест на получение персоны по ID (GetById)
        /// </summary>
        [Fact]
        public async Task GetIdShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var person1 = TestDataGenerator.Person();
            var person2 = TestDataGenerator.Person();

            await context.Persons.AddRangeAsync(person1, person2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Person/{person2.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PersonResponse>(resultString);

            result.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    person2.Id,
                    person2.DateOfBirthday,
                    person2.Passport,
                    person2.Phone
                });

            result.FIO.Should().NotBeNull()
                .And
                .BeEquivalentTo($"{person2.LastName} {person2.FirstName} {person2.Patronymic}");   
        }

        /// <summary>
        /// Тест на получения всех персон (GetAll)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var person1 = TestDataGenerator.Person();
            var person2 = TestDataGenerator.Person(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Persons.AddRangeAsync(person1, person2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Person");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<PersonResponse>>(resultString);

            result.Should().NotBeNull()
                .And
                .Contain(x => x.Id == person1.Id)
                .And
                .NotContain(x => x.Id == person2.Id);
        }

        /// <summary>
        /// Тест на добавление персоны (Add)
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var person = TestDataGenerator.CreatePersonRequest();

            // Act
            string data = JsonConvert.SerializeObject(person);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Person", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CourseResponse>(resultString);

            var personFirst = await context.Persons.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            personFirst.Should()
                .BeEquivalentTo(person);
        }

        /// <summary>
        /// Тест на изменение персоны по ID (Edit)
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var person = TestDataGenerator.Person();
            await context.Persons.AddAsync(person);
            await unitOfWork.SaveChangesAsync();

            var personRequest = TestDataGenerator.PersonRequest(x => x.Id = person.Id);

            // Act
            string data = JsonConvert.SerializeObject(personRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Person", contextdata);

            var personFirst = await context.Persons.FirstAsync(x => x.Id == person.Id);

            // Assert           
            personFirst.Should()
                .BeEquivalentTo(personRequest);
        }

        /// <summary>
        /// Тест на удаление персоны по ID (Delete)
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var person = TestDataGenerator.Person();
            await context.Persons.AddAsync(person);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Person/{person.Id}");

            var personFirst = await context.Persons.FirstAsync(x => x.Id == person.Id);

            // Assert
            personFirst.DeletedAt.Should()
                .NotBeNull();

            person.Should()
                .BeEquivalentTo(new
                {
                    person.LastName,
                    person.FirstName,
                    person.Patronymic,
                    person.DateOfBirthday,
                    person.Passport,
                    person.Phone
                });
        }
    }
}
