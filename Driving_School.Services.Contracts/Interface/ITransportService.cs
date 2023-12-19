using Driving_School.Services.Contracts.Models;
using Driving_School.Services.Contracts.RequestModels;

namespace Driving_School.Services.Contracts.Interface
{
    public interface ITransportService
    {
        /// <summary>
        /// Получить список всех <see cref="TransportModel"/>
        /// </summary>
        Task<IEnumerable<TransportModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TransportModel"/> по идентификатору
        /// </summary>
        Task<TransportModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый транспорт
        /// </summary>
        Task<TransportModel> AddAsync(TransportRequestModel course, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий транспорт
        /// </summary>
        Task<TransportModel> EditAsync(TransportRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий транспорт
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
