using Driving_School.Context.Contracts.Models;

namespace Driving_School.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Lesson"/>
    /// </summary>
    public interface ILessonWriteRepository : IRepositoryWriter<Lesson>
    {
    }
}
