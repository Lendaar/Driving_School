using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts;

namespace Driving_School.Repositories.Implementations
{
    /// <inheritdoc cref="ILessonWriteRepository"/>
    public class LessonWriteRepository : BaseWriteRepository<Lesson>,
        ILessonWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="LessonWriteRepository"/>
        /// </summary>
        public LessonWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
