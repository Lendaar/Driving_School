using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Student"/>
    /// </summary>
    public interface IStudentReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Student"/>
        /// </summary>
        Task<List<Student>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="Student"/> по идентификатору
        /// </summary>
        Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Student"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Student>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);
    }
}
