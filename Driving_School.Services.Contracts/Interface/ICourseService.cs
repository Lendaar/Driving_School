using Driving_School.Services.Contracts.Models;
using Driving_School.Services.Contracts.RequestModels;

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

        /// <summary>
        /// Добавляет новый курс
        /// </summary>
        Task<CourseModel> AddAsync(CourseRequestModel course, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий курс
        /// </summary>
        Task<CourseModel> EditAsync(CourseRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий курс
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}