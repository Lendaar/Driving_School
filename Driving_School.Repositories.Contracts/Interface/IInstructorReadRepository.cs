using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Instructor"/>
    /// </summary>
    public interface IInstructorReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Instructor"/>
        /// </summary>
        Task<List<Instructor>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Instructor"/> по идентификатору
        /// </summary>
        Task<Instructor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Instructor"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Instructor>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);
    }
}
