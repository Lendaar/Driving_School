using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Lesson"/>
    /// </summary>
    public interface ILessonReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Lesson"/>
        /// </summary>
        Task<List<Lesson>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Lesson"/> по идентификатору
        /// </summary>
        Task<Lesson?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
