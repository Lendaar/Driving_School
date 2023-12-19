using AutoMapper;
using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Anchors;
using Driving_School.Services.Contracts.Exceptions;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;
using Driving_School.Services.Contracts.RequestModels;

namespace Driving_School.Services.Implementations
{
    public class PlaceService : IPlaceService, IServiceAnchor
    {
        private readonly IPlaceReadRepository placeReadRepository;
        private readonly IPlaceWriteRepository placeWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PlaceService(IPlaceReadRepository placeReadRepository,
            IMapper mapper,
            IPlaceWriteRepository placeWriteRepository,
            IUnitOfWork unitOfWork)
        {
            this.placeReadRepository = placeReadRepository;
            this.placeWriteRepository = placeWriteRepository;
            this.unitOfWork = unitOfWork;
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

        async Task<PlaceModel> IPlaceService.AddAsync(PlaceRequestModel place, CancellationToken cancellationToken)
        {
            var item = new Place
            {
                Id = Guid.NewGuid(),
                Name = place.Name,
                Description = place.Description,
                Address = place.Address,
            };
            placeWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PlaceModel>(item);
        }

        async Task<PlaceModel> IPlaceService.EditAsync(PlaceRequestModel source, CancellationToken cancellationToken)
        {
            var targetPlace = await placeReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetPlace == null)
            {
                throw new Driving_SchoolEntityNotFoundException<Place>(source.Id);
            }

            targetPlace.Name = source.Name;
            targetPlace.Description = source.Description;
            targetPlace.Address = source.Address;

            placeWriteRepository.Update(targetPlace);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PlaceModel>(targetPlace);
        }

        async Task IPlaceService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetPlace = await placeReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetPlace == null)
            {
                throw new Driving_SchoolEntityNotFoundException<Place>(id);
            }

            if (targetPlace.DeletedAt.HasValue)
            {
                throw new Driving_SchoolInvalidOperationException($"Площадка с идентификатором {id} уже удалена");
            }

            placeWriteRepository.Delete(targetPlace);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
