using FluentAssertions;
using Driving_School.Context.Tests;
using Driving_School.Repositories.Implementations;
using Xunit;
using Driving_School.Repositories.Contracts.Interface;

namespace Driving_School.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="ILessonReadRepository"/>
    /// </summary>
    public class lessonReadRepositoryTests : Driving_SchoolContextInMemory
    {
        private readonly ILessonReadRepository lessonReadRepository;

        public lessonReadRepositoryTests()
        {
            lessonReadRepository = new LessonReadRepositories(Reader);
        }

        /// <summary>
        /// Возвращает пустой список занятий
        /// </summary>
        [Fact]
        public async Task GetAllLessonEmpty()
        {
            // Act
            var result = await lessonReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список занятий
        /// </summary>
        [Fact]
        public async Task GetAllLessonValue()
        {
            //Arrange
            var target = TestDataGenerator.Lesson();
            await Context.Lessons.AddRangeAsync(target,
                TestDataGenerator.Lesson(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await lessonReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение занятия по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdLessonNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await lessonReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение занятия по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdLessonValue()
        {
            //Arrange
            var target = TestDataGenerator.Lesson();
            await Context.Lessons.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await lessonReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }
    }
}