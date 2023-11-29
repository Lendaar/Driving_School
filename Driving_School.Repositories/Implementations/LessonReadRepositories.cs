using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Anchors;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;
using TimeTable203.Common.Entity.Repositories;

namespace Driving_School.Repositories.Implementations
{
    public class LessonReadRepositories : ILessonReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public LessonReadRepositories(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Lesson>> ILessonReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Lesson>()
                .OrderBy(x => x.StartDate)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Lesson?> ILessonReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Lesson>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);
    }
}
