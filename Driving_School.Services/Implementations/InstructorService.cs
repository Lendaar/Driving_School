using AutoMapper;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Anchors;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Implementations
{
    public class InstructorService : IInstructorService, IServiceAnchor
    {
        private readonly IInstructorReadRepository instructorReadRepository;
        private readonly IMapper mapper;

        public InstructorService(IInstructorReadRepository instructorReadRepository, IMapper mapper)
        {
            this.instructorReadRepository = instructorReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<InstructorModel>> IInstructorService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await instructorReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<InstructorModel>>(result);
        }

        async Task<InstructorModel?> IInstructorService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await instructorReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<InstructorModel>(item);
        }
    }
}
