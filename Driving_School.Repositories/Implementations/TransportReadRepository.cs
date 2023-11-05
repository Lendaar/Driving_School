using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Anchors;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Driving_School.Repositories.Implementations
{
    public class TransportReadRepository : ITransportReadRepository, IRepositoriesAnchor
    {
        private readonly IDriving_SchoolContext context;

        public TransportReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Transport>> ITransportReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Transports.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToListAsync();

        Task<Transport?> ITransportReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Transports.FirstOrDefaultAsync(x => x.Id == id);

        Task<Dictionary<Guid, Transport>> ITransportReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => context.Transports.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(x => x.Id);
    }
}
