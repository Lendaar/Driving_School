using AutoMapper;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Anchors;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Implementations
{
    public class StudentService : IStudentService, IServiceAnchor
    {
        private readonly IStudentReadRepository studentReadRepository;
        private readonly IMapper mapper;

        public StudentService(IStudentReadRepository studentReadRepository, IMapper mapper)
        {
            this.studentReadRepository = studentReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<StudentModel>> IStudentService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await studentReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<StudentModel>>(result);
        }

        async Task<StudentModel?> IStudentService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await studentReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<StudentModel>(item);
        }
    }
}
