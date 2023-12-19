using AutoMapper;
using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Repositories.Contracts;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Anchors;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;
using Driving_School.Services.Contracts.Exceptions;
using Driving_School.Context.Contracts.Models;
using Driving_School.Services.Contracts.RequestModels;

namespace Driving_School.Services.Implementations
{
    public class CourseService : ICourseService, IServiceAnchor
    {
        private readonly ICourseReadRepository courseReadRepository;
        private readonly ICourseWriteRepository courseWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CourseService(ICourseReadRepository courseReadRepository,
            IMapper mapper,
            ICourseWriteRepository courseWriteRepository,
            IUnitOfWork unitOfWork)
        {
            this.courseReadRepository = courseReadRepository;
            this.courseWriteRepository = courseWriteRepository;
            this.unitOfWork = unitOfWork;
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
                throw new Driving_SchoolEntityNotFoundException<Course>(id);
            }
            return mapper.Map<CourseModel>(item);
        }

        async Task<CourseModel> ICourseService.AddAsync(CourseRequestModel course, CancellationToken cancellationToken)
        {
            var item = new Course
            {
                Id = Guid.NewGuid(),
                Name = course.Name,
                Description = course.Description,
                Duration = course.Duration,
                Price = course.Price
            };
            courseWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CourseModel>(item);
        }

        async Task<CourseModel> ICourseService.EditAsync(CourseRequestModel source, CancellationToken cancellationToken)
        {
            var targetCourse = await courseReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetCourse == null)
            {
                throw new Driving_SchoolEntityNotFoundException<Course>(source.Id);
            }

            targetCourse.Name = source.Name;
            targetCourse.Description = source.Description;
            targetCourse.Duration = source.Duration;
            targetCourse.Price = source.Price;

            courseWriteRepository.Update(targetCourse);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CourseModel>(targetCourse);
        }

        async Task ICourseService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetCourse = await courseReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetCourse == null)
            {
                throw new Driving_SchoolEntityNotFoundException<Course>(id);
            }

            if (targetCourse.DeletedAt.HasValue)
            {
                throw new Driving_SchoolInvalidOperationException($"Курс с идентификатором {id} уже удален");
            }

            courseWriteRepository.Delete(targetCourse);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
