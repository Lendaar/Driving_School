using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Contracts.Interface
{
    public interface ICourseService
    {
        /// <summary>
        /// Получить список всех <see cref="CourseModel"/>
        /// </summary>
        Task<IEnumerable<CourseModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CourseModel"/> по идентификатору
        /// </summary>
        Task<CourseModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}