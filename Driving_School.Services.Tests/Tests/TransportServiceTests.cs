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
    /// Тесты для <see cref="ITransportService"/>
    /// </summary>
    public class TransportServiceTests : Driving_SchoolContextInMemory
    {
        private readonly ITransportService transportService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TransportServiceTests"/>
        /// </summary>
        public TransportServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            transportService = new TransportService(
                new TransportReadRepository(Reader),
                config.CreateMapper(),
                new TransportWriteRepository(WriterContext),
                UnitOfWork
            );
        }

        /// <summary>
        /// Получение транспорта по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => transportService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<Driving_SchoolEntityNotFoundException<Transport>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение транспорта по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Transport();
            await Context.Transports.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await transportService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Number,
                    target.GSBType
                });
        }
        /// <summary>
        /// Добавление транспорта, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.TransportRequestModel();

            //Act
            var act = await transportService.AddAsync(target,  CancellationToken);

            //Assert
            var entity = Context.Transports.Single(x =>
                x.Id == act.Id &&
                x.Name == target.Name &&
                x.Number == target.Number
            );
            entity.Should().NotBeNull();
        }

        /// <summary>
        /// Изменение транспорта, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Transport();
            await Context.Transports.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.TransportRequestModel();
            targetModel.Id = target.Id;

            //Act
            var act = await transportService.EditAsync(targetModel, CancellationToken);

            //Assert
            var entityTargetModel = Context.Transports.Single(x =>
                x.Id == act.Id &&
                x.Name == targetModel.Name &&
                x.Number == targetModel.Number
            );
            entityTargetModel.Should().NotBeNull();
        }

        /// <summary>
        /// Удаление транспорта, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Transport();
            await Context.Transports.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => transportService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Transports.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}
