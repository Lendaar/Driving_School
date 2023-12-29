using Driving_School.Api.Models;
using Driving_School.Api.Tests.Infrastructures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Driving_School.Api.Tests.IntegrationTests
{
    public class TransportIntegrationTest : BaseClassForIntegrationTests
    {
        public TransportIntegrationTest(Driving_SchoolApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Тест на получение транспорта по ID (GetById)
        /// </summary>
        [Fact]
        public async Task GetIdShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var transport1 = TestDataGenerator.Transport();
            var transport2 = TestDataGenerator.Transport();

            await context.Transports.AddRangeAsync(transport1, transport2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Transport/{transport2.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TransportResponse>(resultString);

            result.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    transport2.Id,
                    transport2.Name,
                    transport2.Number
                });
        }

        /// <summary>
        /// Тест на получения всего транспрота (GetAll)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var transport1 = TestDataGenerator.Transport();
            var transport2 = TestDataGenerator.Transport(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Transports.AddRangeAsync(transport1, transport2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Transport");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<TransportResponse>>(resultString);

            result.Should().NotBeNull()
                .And
                .Contain(x => x.Id == transport1.Id)
                .And
                .NotContain(x => x.Id == transport2.Id);
        }

        /// <summary>
        /// Тест на добавление транспорта (Add)
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var transport = TestDataGenerator.CreateTransportRequest();

            // Act
            string data = JsonConvert.SerializeObject(transport);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Transport", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TransportResponse>(resultString);

            var transportFirst = await context.Transports.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            transportFirst.Should()
                .BeEquivalentTo(transport);
        }

        /// <summary>
        /// Тест на изменение транспорта по ID (Edit)
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var transport = TestDataGenerator.Transport();
            await context.Transports.AddAsync(transport);
            await unitOfWork.SaveChangesAsync();

            var transportRequest = TestDataGenerator.TransportRequest(x => x.Id = transport.Id);

            // Act
            string data = JsonConvert.SerializeObject(transportRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Transport", contextdata);

            var transportFirst = await context.Transports.FirstAsync(x => x.Id == transport.Id);

            // Assert           
            transportFirst.Should()
                .BeEquivalentTo(transportRequest);
        }

        /// <summary>
        /// Тест на удаление транспорта по ID (Delete)
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var trasnport = TestDataGenerator.Transport();
            await context.Transports.AddAsync(trasnport);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Transport/{trasnport.Id}");

            var transportFirst = await context.Transports.FirstAsync(x => x.Id == trasnport.Id);

            // Assert
            transportFirst.DeletedAt.Should()
                .NotBeNull();

            transportFirst.Should()
                .BeEquivalentTo(new
                {
                    trasnport.Name,
                    trasnport.Number,
                    trasnport.GSBType
                });
        }
    }
}
