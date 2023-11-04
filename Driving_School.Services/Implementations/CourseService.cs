using AutoMapper;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseReadRepository courseReadRepository;
        private readonly IMapper mapper;

        public CourseService(ICourseReadRepository courseReadRepository, IMapper mapper)
        {
            this.courseReadRepository = courseReadRepository;
            this.mapper = mapper;
        }

        async Task<IEnumerable<CourseModel>> ICourseService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await courseReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<CourseModel>>(result);
        }

        async Task<CourseModel?> ICourseService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await courseReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<CourseModel>(item);
        }
    }
}
