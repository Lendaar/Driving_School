using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Anchors;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Driving_School.Repositories.Implementations
{
    public class StudentReadRepository : IStudentReadRepository, IRepositoriesAnchor
    {
        private readonly IDriving_SchoolContext context;

        public StudentReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Person>> IStudentReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Students.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.LastName)
                .ToListAsync();

        Task<Person?> IStudentReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Students.FirstOrDefaultAsync(x => x.Id == id);

        Task<Dictionary<Guid, Person>> IStudentReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => context.Students.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.LastName)
                .ToDictionaryAsync(x => x.Id);
    }
}
