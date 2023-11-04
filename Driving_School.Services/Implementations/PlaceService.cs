using AutoMapper;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Anchors;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Implementations
{
    public class PlaceService : IPlaceService, IServiceAnchor
    {
        private readonly IPlaceReadRepository placeReadRepository;
        private readonly IMapper mapper;

        public PlaceService(IPlaceReadRepository placeReadRepository, IMapper mapper)
        {
            this.placeReadRepository = placeReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<PlaceModel>> IPlaceService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await placeReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<PlaceModel>>(result);
        }

        async Task<PlaceModel?> IPlaceService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await placeReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<PlaceModel>(item);
        }
    }
}
