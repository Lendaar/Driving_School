using AutoMapper;
using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Enums;
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
    public class TransportService : ITransportService, IServiceAnchor
    {
        private readonly ITransportReadRepository transportReadRepository;
        private readonly ITransportWriteRepository transportWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TransportService(ITransportReadRepository transportReadRepository,
            IMapper mapper,
            ITransportWriteRepository transportWriteRepository,
            IUnitOfWork unitOfWork)
        {
            this.transportReadRepository = transportReadRepository;
            this.transportWriteRepository = transportWriteRepository;
            this.unitOfWork = unitOfWork;
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
                throw new Driving_SchoolEntityNotFoundException<Transport>(id);
            }
            return mapper.Map<TransportModel>(item);
        }

        async Task<TransportModel> ITransportService.AddAsync(TransportRequestModel transport, CancellationToken cancellationToken)
        {
            var item = new Transport
            {
                Id = Guid.NewGuid(),
                Name = transport.Name,
                Number = transport.Number,
                GSBType = (GSBTypes)transport.GSBType,
            };
            transportWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<TransportModel>(item);
        }

        async Task<TransportModel> ITransportService.EditAsync(TransportRequestModel source, CancellationToken cancellationToken)
        {
            var targetTransport = await transportReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetTransport == null)
            {
                throw new Driving_SchoolEntityNotFoundException<Transport>(source.Id);
            }

            targetTransport.Name = source.Name;
            targetTransport.Number = source.Number;
            targetTransport.GSBType = (GSBTypes)source.GSBType;

            transportWriteRepository.Update(targetTransport);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<TransportModel>(targetTransport);
        }

        async Task ITransportService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetTransport = await transportReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetTransport == null)
            {
                throw new Driving_SchoolEntityNotFoundException<Course>(id);
            }

            if (targetTransport.DeletedAt.HasValue)
            {
                throw new Driving_SchoolInvalidOperationException($"Транспорт с идентификатором {id} уже удален");
            }

            transportWriteRepository.Delete(targetTransport);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
