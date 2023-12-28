using Driving_School.Context.Tests;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Repositories.Implementations;
using FluentAssertions;
using Xunit;

namespace Driving_School.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="ITransportReadRepository"/>
    /// </summary>
    public class TransportReadRepositoryTests : Driving_SchoolContextInMemory
    {
        private readonly ITransportReadRepository transportReadRepository;

        public TransportReadRepositoryTests()
        {
            transportReadRepository = new TransportReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список транспорта
        /// </summary>
        [Fact]
        public async Task GetAllTransportEmpty()
        {
            // Act
            var result = await transportReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список транспорта
        /// </summary>
        [Fact]
        public async Task GetAllTransportsValue()
        {
            //Arrange
            var target = TestDataGenerator.Transport();
            await Context.Transports.AddRangeAsync(target,
                TestDataGenerator.Transport(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await transportReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение транспорта по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdTransportNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await transportReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение транспорта по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdTransportValue()
        {
            //Arrange
            var target = TestDataGenerator.Transport();
            await Context.Transports.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await transportReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка транспорта по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsTransportEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await transportReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка транспорта по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsTransportsValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Transport();
            var target2 = TestDataGenerator.Transport(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Transport();
            var target4 = TestDataGenerator.Transport();
            await Context.Transports.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await transportReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск транспорта в коллекции по идентификатору (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Transport();
            await Context.Transports.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await transportReadRepository.AnyByIdAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск транспорта в коллекции по идентификатору (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            // Act
            var result = await transportReadRepository.AnyByIdAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного транспорта в коллекции по идентификатору
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Transport(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Transports.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await transportReadRepository.AnyByIdAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }
    }
}
