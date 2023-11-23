using AutoMapper;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Anchors;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Implementations
{
    public class EmployeeService : IEmployeeService, IServiceAnchor
    {
        private readonly IEmployeeReadRepository instructorReadRepository;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeReadRepository instructorReadRepository, IMapper mapper)
        {
            this.instructorReadRepository = instructorReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<EmployeeModel>> IEmployeeService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await instructorReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<EmployeeModel>>(result);
        }

        async Task<EmployeeModel?> IEmployeeService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await instructorReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<EmployeeModel>(item);
        }
    }
}
