using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts;

namespace Driving_School.Repositories.Implementations
{
    /// <inheritdoc cref="IPlaceWriteRepository"/>
    public class PlaceWriteRepository : BaseWriteRepository<Place>,
        IPlaceWriteRepository,
        IRepositoriesAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PlaceWriteRepository"/>
        /// </summary>
        public PlaceWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
