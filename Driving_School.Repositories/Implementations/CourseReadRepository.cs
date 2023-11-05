using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Anchors;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Driving_School.Repositories.Implementations
{
    public class CourseReadRepository : ICourseReadRepository, IRepositoriesAnchor
    {
        private readonly IDriving_SchoolContext context;

        public CourseReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Course>> ICourseReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Courses.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Name)
                .ToListAsync();

        Task<Course?> ICourseReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Courses.FirstOrDefaultAsync(x => x.Id == id);

        Task<Dictionary<Guid, Course>> ICourseReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => context.Courses.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(x => x.Id);
    }
}
