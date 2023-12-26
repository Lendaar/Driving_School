using Driving_School.Context.Tests;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Repositories.Implementations;
using FluentAssertions;
using Xunit;

namespace Driving_School.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IPlaceReadRepository"/>
    /// </summary>
    public class PlaceReadRepositoryTests : Driving_SchoolContextInMemory
    {
        private readonly IPlaceReadRepository placeReadRepository;

        public PlaceReadRepositoryTests()
        {
            placeReadRepository = new PlaceReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список площадок
        /// </summary>
        [Fact]
        public async Task GetAllPlaceEmpty()
        {
            // Act
            var result = await placeReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список площадок
        /// </summary>
        [Fact]
        public async Task GetAllPlacesValue()
        {
            //Arrange
            var target = TestDataGenerator.Place();
            await Context.Places.AddRangeAsync(target,
                TestDataGenerator.Place(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await placeReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение площадки по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdPlaceNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await placeReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение площадки по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdPlaceValue()
        {
            //Arrange
            var target = TestDataGenerator.Place();
            await Context.Places.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await placeReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }


        /// <summary>
        /// Получение списка площадок по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsPlaceEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await placeReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка площадок по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsPalcesValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Place();
            var target2 = TestDataGenerator.Place(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Place();
            var target4 = TestDataGenerator.Place();
            await Context.Places.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await placeReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }
    }
}