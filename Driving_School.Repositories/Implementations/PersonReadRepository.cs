using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Anchors;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;

namespace Driving_School.Repositories.Implementations
{
    public class PersonReadRepository : IPersonReadRepository, IRepositoriesAnchor
    {
        private readonly IDriving_SchoolContext context;

        public PersonReadRepository(IDriving_SchoolContext context)
        {
            this.context = context;
        }

        Task<List<Person>> IPersonReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => context.Persons.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.LastName)
                .ToListAsync();

        Task<Person?> IPersonReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => context.Persons.FirstOrDefaultAsync(x => x.Id == id);

        Task<Dictionary<Guid, Person>> IPersonReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => context.Persons.Where(x => x.DeletedAt == null && ids.Contains(x.Id))
                .OrderBy(x => x.LastName)
                .ToDictionaryAsync(x => x.Id);
    }
}
