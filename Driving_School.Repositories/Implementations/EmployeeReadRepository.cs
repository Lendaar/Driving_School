using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Enums;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts.Interface;
using Microsoft.EntityFrameworkCore;
using TimeTable203.Common.Entity.Repositories;

namespace Driving_School.Repositories.Implementations
{
    public class EmployeeReadRepository : IEmployeeReadRepository, IRepositoriesAnchor
    {
        private readonly IDbRead reader;

        public EmployeeReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Employee>> IEmployeeReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .OrderBy(x => x.EmployeeType)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Employee?> IEmployeeReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Employee>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Employee>> IEmployeeReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
             => reader.Read<Employee>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.EmployeeType)
                .ToDictionaryAsync(key => key.Id, cancellation);

        public Task<Dictionary<Guid, Person?>> GetPersonByEmployeeIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .ByIds(ids)
                .Select(x => new
                {
                    x.Id,
                    x.Person,
                })
                .ToDictionaryAsync(key => key.Id, val => val.Person, cancellation);

        Task<bool> IEmployeeReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<Employee>()
                 .NotDeletedAt()
                 .ById(id)
                 .AnyAsync(cancellationToken);

        Task<bool> IEmployeeReadRepository.AnyByIdWithInstructorAsync(Guid id, CancellationToken cancellationToken)
               => reader.Read<Employee>()
                   .NotDeletedAt()
                   .ById(id)
                   .AnyAsync(x => x.EmployeeType == EmployeeTypes.Instructor, cancellationToken);

        Task<bool> IEmployeeReadRepository.AnyByIdWithStudentAsync(Guid id, CancellationToken cancellationToken)
               => reader.Read<Employee>()
                   .NotDeletedAt()
                   .ById(id)
                   .AnyAsync(x => x.EmployeeType == EmployeeTypes.Student, cancellationToken);
    }
}
