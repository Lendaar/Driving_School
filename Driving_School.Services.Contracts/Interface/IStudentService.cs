using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Contracts.Interface
{
    public interface IStudentService
    {
        /// <summary>
        /// Получить список всех <see cref="StudentModel"/>
        /// </summary>
        Task<IEnumerable<StudentModel>> GetAllAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить <see cref="StudentModel"/> по идентификатору
        /// </summary>
        Task<StudentModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
