using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Transport"/>
    /// </summary>
    public interface ITransportWriteRepository : IRepositoryWriter<Transport>
    {
    }
}
