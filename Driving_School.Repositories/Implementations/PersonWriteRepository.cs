using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts;

namespace Driving_School.Repositories.Implementations
{
    /// <inheritdoc cref="IPersonWriteRepository"/>
    public class PersonWriteRepository : BaseWriteRepository<Person>,
        IPersonWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PersonWriteRepository"/>
        /// </summary>
        public PersonWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
