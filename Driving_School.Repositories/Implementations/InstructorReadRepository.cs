using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Anchors;
using Driving_School.Repositories.Contracts.Interface;

namespace Driving_School.Repositories.Implementations
{
    public class InstructorReadRepository : IInstructorReadRepository, IRepositoriesAnchor
    {
        private readonly IDriving_SchoolContext context;

        public InstructorReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Instructor>> IInstructorReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Instructors.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Person.LastName)
                .ToList());

        Task<Instructor?> IInstructorReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Instructors.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Instructor>> IInstructorReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => Task.FromResult(context.Instructors.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Person.LastName)
                .ToDictionary(x => x.Id));
    }
}
