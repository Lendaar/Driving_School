using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts;

namespace Driving_School.Repositories.Implementations
{
    /// <inheritdoc cref="ITransportWriteRepository"/>
    public class TransportWriteRepository : BaseWriteRepository<Transport>,
        ITransportWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TransportWriteRepository"/>
        /// </summary>
        public TransportWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
