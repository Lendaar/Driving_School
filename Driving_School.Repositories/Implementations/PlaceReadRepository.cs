using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;
using TimeTable203.Common.Entity.Repositories;

namespace Driving_School.Repositories.Implementations
{
    public class PlaceReadRepository : IPlaceReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public PlaceReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Place>> IPlaceReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Place>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Place?> IPlaceReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Place>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Place>> IPlaceReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Place>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> IPlaceReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Place>()
                 .NotDeletedAt()
                 .ById(id)
                 .AnyAsync(cancellationToken);
    }
}
