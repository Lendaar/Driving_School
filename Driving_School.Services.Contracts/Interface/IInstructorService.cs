using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Contracts.Interface
{
    public interface IInstructorService
    {
        /// <summary>
        /// Получить список всех <see cref="InstructorModel"/>
        /// </summary>
        Task<IEnumerable<InstructorModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="InstructorModel"/> по идентификатору
        /// </summary>
        Task<InstructorModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
