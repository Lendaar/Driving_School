using AutoMapper;
using Driving_School.Context.Contracts.Models;
using Driving_School.Context.Tests;
using Driving_School.Repositories.Implementations;
using Driving_School.Services.Automappers;
using Driving_School.Services.Contracts.Exceptions;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Implementations;
using FluentAssertions;
using Xunit;

namespace Driving_School.Services.Tests.Tests
{
    public class PersonServiceTests : Driving_SchoolContextInMemory
    {
        private readonly IPersonService personService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PersonServiceTests"/>
        /// </summary>

        public PersonServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            personService = new PersonService(
                new PersonReadRepository(Reader),
                config.CreateMapper(),
                new PersonWriteRepository(WriterContext),
                UnitOfWork
            );
        }

        /// <summary>
        /// Получение персоны по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => personService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<Driving_SchoolEntityNotFoundException<Person>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение персоны по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.Persons.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await personService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.LastName,
                    target.FirstName,
                    target.Patronymic,
                    target.DateOfBirthday,
                    target.Passport,
                    target.Phone
                });
        }

        // <summary>
        /// Добавление персоны, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.PersonRequestModel();

            //Act
            var act = await personService.AddAsync(target, CancellationToken);

            //Assert
            var entity = Context.Persons.Single(x =>
                x.Id == act.Id &&
                x.LastName == target.LastName
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Изменение персоны, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.Persons.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.PersonRequestModel();
            targetModel.Id = target.Id;
            targetModel.Patronymic = null;
            //Act
            var act = await personService.EditAsync(targetModel, CancellationToken);

            //Assert

            var entity = Context.Persons.Single(x =>
                x.Id == act.Id &&
                x.LastName == targetModel.LastName &&
                x.Patronymic == null
            );
            entity.Should().NotBeNull();

        }

        /// <summary>
        /// Удаление персоны, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Person();
            await Context.Persons.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => personService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Persons.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}

