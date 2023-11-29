using Driving_School.Services.Contracts.Models;
using Driving_School.Services.Contracts.RequestModels;

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

        /// <summary>
        /// Добавляет новую площадку
        /// </summary>
        Task<PlaceModel> AddAsync(PlaceRequestModel course, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую площадку
        /// </summary>
        Task<PlaceModel> EditAsync(PlaceRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую площадку
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
