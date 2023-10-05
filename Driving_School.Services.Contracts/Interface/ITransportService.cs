using Driving_School.Services.Contracts.Models;

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
    }
}
