using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Anchors;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Driving_School.Repositories.Implementations
{
    public class LessonReadRepositories : ILessonReadRepository, IRepositoriesAnchor
    {
        private readonly IDriving_SchoolContext context;

        public LessonReadRepositories(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Lesson>> ILessonReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Lessons.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.StartDate)
                .ToListAsync();

        Task<Lesson?> ILessonReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Lessons.FirstOrDefaultAsync(x => x.Id == id);
    }
}
