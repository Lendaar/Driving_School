using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts.Interface;

namespace Driving_School.Repositories.Implementations
{
    public class StudentReadRepository : IStudentReadRepository
    {
        private readonly IDriving_SchoolContext context;

        public StudentReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Student>> IStudentReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Students.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.LastName)
                .ToList());

        Task<Student?> IStudentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Students.FirstOrDefault(x => x.Id == id));

    }
}
