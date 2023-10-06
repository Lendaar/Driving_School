using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts.Interface;

namespace Driving_School.Repositories.Implementations
{
    public class CourseReadRepository : ICourseReadRepository
    {
        private readonly IDriving_SchoolContext context;

        public CourseReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Course>> ICourseReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Courses.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToList());

        Task<Course?> ICourseReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Courses.FirstOrDefault(x => x.Id == id));

        Task<List<Course>> ICourseReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => Task.FromResult(context.Courses.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToList());
    }
}
