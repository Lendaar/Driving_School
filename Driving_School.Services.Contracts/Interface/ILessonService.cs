using Driving_School.Services.Contracts.Models;
using Driving_School.Services.Contracts.RequestModels;

namespace Driving_School.Services.Contracts.Interface
{
    public interface ILessonService
    {
        /// <summary>
        /// Получить список всех <see cref="LessonModel"/>
        /// </summary>
        Task<IEnumerable<LessonModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="LessonModel"/> по идентификатору
        /// </summary>
        Task<LessonModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новое занятие
        /// </summary>
        Task<LessonModel> AddAsync(LessonRequestModel course, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующее занятие
        /// </summary>
        Task<LessonModel> EditAsync(LessonRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующее занятие
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
