using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts.Interface;

namespace Driving_School.Repositories.Implementations
{
    public class PlaceReadRepository : IPlaceReadRepository
    {
        private readonly IDriving_SchoolContext context;

        public PlaceReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Place>> IPlaceReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Places.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Place?> IPlaceReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Places.FirstOrDefault(x => x.Id == id));

    }
}
