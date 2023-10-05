using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Contracts.Interface
{
    public interface IPlaceService
    {
        /// <summary>
        /// Получить список всех <see cref="PlaceModel"/>
        /// </summary>
        Task<IEnumerable<PlaceModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="PlaceModel"/> по идентификатору
        /// </summary>
        Task<PlaceModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
