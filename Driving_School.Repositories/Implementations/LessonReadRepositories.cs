using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Anchors;
using Driving_School.Repositories.Contracts.Interface;

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
            => Task.FromResult(context.Lessons.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.StartDate)
                .ToList());

        Task<Lesson?> ILessonReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Lessons.FirstOrDefault(x => x.Id == id));
    }
}
