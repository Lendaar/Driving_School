using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Course"/>
    /// </summary>
    public interface ICourseWriteRepository : IRepositoryWriter<Course>
    {
    }
}
