using Driving_School.Api.Models;
using Driving_School.Api.Tests.Infrastructures;
using Driving_School.Context.Contracts.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Driving_School.Api.Tests.IntegrationTests
{
    public class EmployeeIntegrationTest : BaseClassForIntegrationTests
    {
        private readonly Person person1;
        private readonly Person person2;
        public EmployeeIntegrationTest(Driving_SchoolApiFixture fixture) : base(fixture)
        {
            person1 = TestDataGenerator.Person();
            person2 = TestDataGenerator.Person();
            context.Persons.AddRangeAsync(person1, person2);
            unitOfWork.SaveChangesAsync().Wait();
        }

        /// <summary>
        /// Тест на получение работника по ID (GetById)
        /// </summary>
        [Fact]
        public async Task GetIdShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var employee1 = TestDataGenerator.Employee();
            var employee2 = TestDataGenerator.Employee();

            employee1.PersonId = person1.Id;
            employee2.PersonId = person2.Id;

            await context.Employees.AddRangeAsync(employee1, employee2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Employee/{employee2.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EmployeeResponse>(resultString);

            result.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    employee2.Id,
                    employee2.Email,
                    employee2.Experience,
                    employee2.Number
                });
        }

        /// <summary>
        /// Тест на получения всех работников (GetAll)
        /// </summary>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var employee1 = TestDataGenerator.Employee();
            var employee2 = TestDataGenerator.Employee(x => x.DeletedAt = DateTimeOffset.Now);

            employee1.PersonId = person1.Id;
            employee2.PersonId = person2.Id;

            await context.Employees.AddRangeAsync(employee1, employee2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Employee");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<EmployeeResponse>>(resultString);

            result.Should().NotBeNull()
                .And
                .Contain(x => x.Id == employee1.Id)
                .And
                .NotContain(x => x.Id == employee2.Id);
        }

        /// <summary>
        /// Тест на добавление работника (Add)
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var employee = TestDataGenerator.CreateEmployeeRequest();

            employee.Person = person1.Id;

            // Act
            string data = JsonConvert.SerializeObject(employee);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Employee", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EmployeeResponse>(resultString);

            var employeeFirst = await context.Employees.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            employeeFirst.PersonId.Should().Be(employee.Person);
        }

        /// <summary>
        /// Тест на изменение работника по ID (Edit)
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var employee = TestDataGenerator.Employee();
            employee.PersonId = person1.Id;
            await context.Employees.AddAsync(employee);
            await unitOfWork.SaveChangesAsync();

            var employeeRequest = TestDataGenerator.EmployeeRequest(x => x.Id = employee.Id);

            // Act
            string data = JsonConvert.SerializeObject(employeeRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Employee", contextdata);

            var employeeFirst = await context.Employees.FirstAsync(x => x.Id == employee.Id);

            // Assert           
            employeeFirst.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    employee.Id,
                    employee.PersonId,
                    employee.Experience,
                    employee.Number,
                    employee.Email
                });
        }

        /// <summary>
        /// Тест на удаление работника по ID (Delete)
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var employee = TestDataGenerator.Employee();

            employee.PersonId = person1.Id;

            await context.Employees.AddAsync(employee);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Employee/{employee.Id}");

            var employeeFirst = await context.Employees.FirstAsync(x => x.Id == employee.Id);

            // Assert
            employeeFirst.DeletedAt.Should()
                .NotBeNull();

            employeeFirst.Should()
                .BeEquivalentTo(new
                {
                    employee.Email,
                    employee.Experience,
                    employee.Number,
                    employee.EmployeeType
                });
        }
    }
}
