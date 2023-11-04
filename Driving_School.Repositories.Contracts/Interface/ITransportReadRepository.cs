using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Transport"/>
    /// </summary>
    public interface ITransportReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Transport"/>
        /// </summary>
        Task<List<Transport>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Transport"/> по идентификатору
        /// </summary>
        Task<Transport?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Transport"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Transport>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);
    }
}
