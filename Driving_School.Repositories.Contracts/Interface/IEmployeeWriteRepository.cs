using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Employee"/>
    /// </summary>

    public interface IEmployeeWriteRepository : IRepositoryWriter<Employee>
    {
    }
}
