using AutoMapper;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Anchors;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Implementations
{
    public class PersonService : IPersonService, IServiceAnchor
    {
        private readonly IPersonReadRepository studentReadRepository;
        private readonly IMapper mapper;

        public PersonService(IPersonReadRepository studentReadRepository, IMapper mapper)
        {
            this.studentReadRepository = studentReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<PersonModel>> IPersonService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await studentReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<PersonModel>>(result);
        }

        async Task<PersonModel?> IPersonService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await studentReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<PersonModel>(item);
        }
    }
}
