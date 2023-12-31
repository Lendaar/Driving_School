﻿using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;
using TimeTable203.Common.Entity.Repositories;

namespace Driving_School.Repositories.Implementations
{
    public class PersonReadRepository : IPersonReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public PersonReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Person>> IPersonReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Person>()
                .NotDeletedAt()
                .OrderBy(x => x.LastName)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Person?> IPersonReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Person>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Person>> IPersonReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Person>()
            .NotDeletedAt()
            .ByIds(ids)
            .OrderBy(x => x.LastName)
            .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> IPersonReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Person>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);
    }
}
