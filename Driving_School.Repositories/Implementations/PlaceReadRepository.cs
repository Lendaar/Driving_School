using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Anchors;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Driving_School.Repositories.Implementations
{
    public class PlaceReadRepository : IPlaceReadRepository, IRepositoriesAnchor
    {
        private readonly IDriving_SchoolContext context;

        public PlaceReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Place>> IPlaceReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Places.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToListAsync();

        Task<Place?> IPlaceReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Places.FirstOrDefaultAsync(x => x.Id == id);

        Task<Dictionary<Guid, Place>> IPlaceReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => context.Places.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(x => x.Id);
    }
}
