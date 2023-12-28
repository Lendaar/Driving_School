﻿using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Person"/>
    /// </summary>
    public interface IPersonReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Person"/>
        /// </summary>
        Task<IReadOnlyCollection<Person>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="Person"/> по идентификатору
        /// </summary>
        Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Person"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Person>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Проверка есть ли <see cref="Person"/> по указанному id
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
