using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts;

namespace Driving_School.Repositories.Implementations
{
    /// <inheritdoc cref="IEmployeeWriteRepository"/>
    public class EmployeeWriteRepository : BaseWriteRepository<Employee>,
        IEmployeeWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeWriteRepository"/>
        /// </summary>
        public EmployeeWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
