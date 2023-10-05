using Driving_School.Services.Contracts.Models;

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
    }
}
