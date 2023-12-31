﻿using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Place"/>
    /// </summary>
    public interface IPlaceReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Place"/>
        /// </summary>
        Task<IReadOnlyCollection<Place>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Place"/> по идентификатору
        /// </summary>
        Task<Place?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Place"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Place>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Проверка есть ли <see cref="Place"/> по указанному id
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
