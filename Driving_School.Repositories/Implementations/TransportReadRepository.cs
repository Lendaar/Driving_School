using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts.Interface;

namespace Driving_School.Repositories.Implementations
{
    public class TransportReadRepository : ITransportReadRepository
    {
        private readonly IDriving_SchoolContext context;

        public TransportReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Transport>> ITransportReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Transports.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Transport?> ITransportReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Transports.FirstOrDefault(x => x.Id == id));

        Task<List<Transport>> ITransportReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => Task.FromResult(context.Transports.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToList());
    }
}
