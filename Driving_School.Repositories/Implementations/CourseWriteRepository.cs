using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts;

namespace Driving_School.Repositories.Implementations
{
    /// <inheritdoc cref="ICourseWriteRepository"/>
    public class CourseWriteRepository : BaseWriteRepository<Course>,
        ICourseWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CourseWriteRepository"/>
        /// </summary>
        public CourseWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
