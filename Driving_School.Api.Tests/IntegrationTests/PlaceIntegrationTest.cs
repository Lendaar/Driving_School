using Driving_School.Api.Models;
using Driving_School.Api.Tests.Infrastructures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace Driving_School.Api.Tests.IntegrationTests
{
    public class PlaceIntegrationTest : BaseClassForIntegrationTests
    {
        public PlaceIntegrationTest(Driving_SchoolApiFixture fixture) : base(fixture)
        {
        }

        /// <summary>
        /// Тест на получение площадки по ID (GetById)
        /// </summary>
        [Fact]
        public async Task GetIdShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var place1 = TestDataGenerator.Place();
            var place2 = TestDataGenerator.Place();

            await context.Places.AddRangeAsync(place1, place2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Place/{place2.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PlaceResponse>(resultString);

            result.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    place2.Id,
                    place2.Name,
                    place2.Description,
                    place2.Address,
                });
        }

        /// <summary>
        /// Тест на получения всех площадок (GetAll)
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var place1 = TestDataGenerator.Place();
            var place2 = TestDataGenerator.Place(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Places.AddRangeAsync(place1, place2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Place");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<PlaceResponse>>(resultString);

            result.Should().NotBeNull()
                .And
                .Contain(x => x.Id == place1.Id)
                .And
                .NotContain(x => x.Id == place2.Id);
        }

        /// <summary>
        /// Тест на добавление площадки (Add)
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var place = TestDataGenerator.CreatePlaceRequest();

            // Act
            string data = JsonConvert.SerializeObject(place);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Place", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PlaceResponse>(resultString);

            var placeFirst = await context.Places.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            placeFirst.Should()
                .BeEquivalentTo(place);
        }

        /// <summary>
        /// Тест на изменение площадки по ID (Edit)
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var place = TestDataGenerator.Place();
            await context.Places.AddAsync(place);
            await unitOfWork.SaveChangesAsync();

            var placeRequest = TestDataGenerator.PlaceRequest(x => x.Id = place.Id);

            // Act
            string data = JsonConvert.SerializeObject(placeRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Place", contextdata);

            var placeFirst = await context.Places.FirstAsync(x => x.Id == place.Id);

            // Assert           
            placeFirst.Should()
                .BeEquivalentTo(placeRequest);
        }

        /// <summary>
        /// Тест на удаление площадки по ID (Delete)
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var place = TestDataGenerator.Place();
            await context.Places.AddAsync(place);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Place/{place.Id}");

            var placeFirst = await context.Places.FirstAsync(x => x.Id == place.Id);

            // Assert
            placeFirst.DeletedAt.Should()
                .NotBeNull();

            place.Should()
                .BeEquivalentTo(new
                {
                    place.Name,
                    place.Description,
                    place.Address,
                });
        }
    }
}
