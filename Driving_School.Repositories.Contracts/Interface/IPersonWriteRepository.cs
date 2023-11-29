using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Person"/>
    /// </summary>
    public interface IPersonWriteRepository : IRepositoryWriter<Person>
    {
    }
}
