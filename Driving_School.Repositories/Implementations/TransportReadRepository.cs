using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;
using TimeTable203.Common.Entity.Repositories;

namespace Driving_School.Repositories.Implementations
{
    public class TransportReadRepository : ITransportReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public TransportReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Transport>> ITransportReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Transport>()
                 .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Transport?> ITransportReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Transport>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Transport>> ITransportReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Transport>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(key => key.Id, cancellation);
    }
}
