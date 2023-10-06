using AutoMapper;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Implementations
{
    public class TransportService : ITransportService
    {
        private readonly ITransportReadRepository transportReadRepository;
        private readonly IMapper mapper;

        public TransportService(ITransportReadRepository transportReadRepository, IMapper mapper)
        {
            this.transportReadRepository = transportReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<TransportModel>> ITransportService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await transportReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<TransportModel>>(result);
        }

        async Task<TransportModel?> ITransportService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await transportReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<TransportModel>(item);
        }
    }
}
