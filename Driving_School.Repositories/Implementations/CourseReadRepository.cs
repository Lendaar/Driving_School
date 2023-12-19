using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;
using TimeTable203.Common.Entity.Repositories;

namespace Driving_School.Repositories.Implementations
{
    public class CourseReadRepository : ICourseReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public CourseReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Course>> ICourseReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Course>()
                .NotDeletedAt()
                .OrderBy(x => x.Name)
                .ToReadOnlyCollectionAsync(cancellationToken);


        Task<Course?> ICourseReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Course>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Course>> ICourseReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Course>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(key => key.Id, cancellation);
    }
}
