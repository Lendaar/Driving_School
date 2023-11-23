using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Anchors;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Driving_School.Repositories.Implementations
{
    public class EmployeeReadRepository : IEmployeeReadRepository, IRepositoriesAnchor
    {
        private readonly IDriving_SchoolContext context;

        public EmployeeReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Employee>> IEmployeeReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Employees.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.Person.LastName)
                .ToListAsync();

        Task<Employee?> IEmployeeReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Employees.FirstOrDefaultAsync(x => x.Id == id);

        Task<Dictionary<Guid, Employee>> IEmployeeReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => context.Employees.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.Person.LastName)
                .ToDictionaryAsync(x => x.Id);
    }
}
