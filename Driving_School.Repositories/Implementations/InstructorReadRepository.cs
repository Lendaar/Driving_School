using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts.Interface;

namespace Driving_School.Repositories.Implementations
{
    public class InstructorReadRepository : IInstructorReadRepository
    {
        private readonly IDriving_SchoolContext context;

        public InstructorReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Instructor>> IInstructorReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Instructors.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.LastName)
                .ToList());

        Task<Instructor?> IInstructorReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Instructors.FirstOrDefault(x => x.Id == id));

    }
}
