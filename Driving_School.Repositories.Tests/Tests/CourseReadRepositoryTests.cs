using Driving_School.Context.Tests;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Repositories.Implementations;
using FluentAssertions;
using Xunit;

namespace Driving_School.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="ICourseReadRepository"/>
    /// </summary>
    public class CourseReadRepositoryTests : Driving_SchoolContextInMemory
    {
        private readonly ICourseReadRepository courseReadRepository;

        public CourseReadRepositoryTests()
        {
            courseReadRepository = new CourseReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список курсов
        /// </summary>
        [Fact]
        public async Task GetAllCourseEmpty()
        {
            // Act
            var result = await courseReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращает список курсов
        /// </summary>
        [Fact]
        public async Task GetAllCourseValue()
        {
            //Arrange
            var target = TestDataGenerator.Course();
            await Context.Courses.AddRangeAsync(target,
                TestDataGenerator.Course(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await courseReadRepository.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение курса по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdCourseNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            var result = await courseReadRepository.GetByIdAsync(id, CancellationToken);

            // Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение курса по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdCourseValue()
        {
            //Arrange
            var target = TestDataGenerator.Course();
            await Context.Courses.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await courseReadRepository.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка курсов по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsCoursesEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            // Act
            var result = await courseReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка курсов по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsCoursesValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Course();
            var target2 = TestDataGenerator.Course(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Course();
            var target4 = TestDataGenerator.Course();
            await Context.Courses.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await courseReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }
    }
}