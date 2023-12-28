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
    /// Тесты для <see cref="ICourseService"/>
    /// </summary>
    public class CourseServiceTests : Driving_SchoolContextInMemory
    {
        private readonly ICourseService courseService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CourseServiceTests"/>
        /// </summary>
        public CourseServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            courseService = new CourseService(
                new CourseReadRepository(Reader),
                config.CreateMapper(),
                new CourseWriteRepository(WriterContext),
                UnitOfWork
            );
        }

        /// <summary>
        /// Получение курса по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> act = () => courseService.GetByIdAsync(id, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<Driving_SchoolEntityNotFoundException<Course>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение курса по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Course();
            await Context.Courses.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await courseService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Description,
                    target.Duration,
                    target.Price
                });
        }
        /// <summary>
        /// Добавление курса, возвращает данные
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.CourseRequestModel();

            //Act
            var act = await courseService.AddAsync(target,  CancellationToken);

            //Assert
            var entity = Context.Courses.Single(x =>
                x.Id == act.Id &&
                x.Name == target.Name &&
                x.Description == target.Description &&
                x.Duration == target.Duration &&
                x.Price == target.Price
            );
            entity.Should().NotBeNull();
        }

        /// <summary>
        /// Изменение курса, изменяет данные
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Course();
            await Context.Courses.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var targetModel = TestDataGenerator.CourseRequestModel();
            targetModel.Id = target.Id;

            //Act
            var act = await courseService.EditAsync(targetModel, CancellationToken);

            //Assert
            var entityTargetModel = Context.Courses.Single(x =>
                x.Id == act.Id &&
                x.Name == targetModel.Name &&
                x.Description == targetModel.Description &&
                x.Duration == targetModel.Duration &&
                x.Price == targetModel.Price
            );
            entityTargetModel.Should().NotBeNull();
        }

        /// <summary>
        /// Удаление курса, возвращает пустоту
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var target = TestDataGenerator.Course();
            await Context.Courses.AddAsync(target);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> act = () => courseService.DeleteAsync(target.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Courses.Single(x => x.Id == target.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }
    }
}
