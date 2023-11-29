﻿using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Course"/>
    /// </summary>
    public interface ICourseReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Course"/>
        /// </summary>
        Task<IReadOnlyCollection<Course>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Course"/> по идентификатору
        /// </summary>
        Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Course"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Course>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);
    }
}