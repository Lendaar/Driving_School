using AutoMapper;
using FluentAssertions;
using Driving_School.Context.Contracts.Models;
using Driving_School.Context.Tests;
using Driving_School.Repositories.Implementations;
using Driving_School.Services.Automappers;
using Driving_School.Services.Contracts.Exceptions;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Implementations;
using Xunit;

namespace Driving_School.Services.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IPlaceService"/>
    /// </summary>
    public class PlaceServiceTests : Driving_SchoolContextInMemory
    {
        private readonly IPlaceService placeService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PlaceServiceTests"/>
        /// </summary>
        public PlaceServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            placeService = new PlaceService(
                new PlaceReadRepository(Reader),
                config.CreateMapper(),
                new PlaceWriteRepository(WriterContext),
                UnitOfWork
            );
        }

        /// <summary>
        /// Получение площадки по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => placeService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<Driving_SchoolEntityNotFoundException<Place>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение площадки по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Place();
            await Context.Places.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await placeService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Description,
                    target.Address
                });
        }
        /// <summary>
        /// Добавление площадки, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.PlaceRequestModel();

            //Act
            var act = await placeService.AddAsync(target,  CancellationToken);

            //Assert
            var entity = Context.Places.Single(x =>
                x.Id == act.Id &&
                x.Name == target.Name &&
                x.Description == target.Description &&
                x.Address == target.Address
            );
            entity.Should().NotBeNull();
        }

        /// <summary>
        /// Изменение площадки, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Place();
            await Context.Places.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.PlaceRequestModel();
            targetModel.Id = target.Id;

            //Act
            var act = await placeService.EditAsync(targetModel, CancellationToken);

            //Assert
            var entityTargetModel = Context.Places.Single(x =>
                x.Id == act.Id &&
                x.Name == targetModel.Name &&
                x.Description == targetModel.Description &&
                x.Address == targetModel.Address
            );
            entityTargetModel.Should().NotBeNull();
        }

        /// <summary>
        /// Удаление площадки, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Place();
            await Context.Places.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => placeService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Places.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}
