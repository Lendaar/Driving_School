using Driving_School.Context.Contracts.Models;
using Driving_School.Context.Contracts.Enums;

namespace Driving_School.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Employee"/>
    /// </summary>
    public interface IEmployeeReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Employee"/>
        /// </summary>
        Task<IReadOnlyCollection<Employee>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Employee"/> по идентификатору
        /// </summary>
        Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Employee"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Employee>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Получить список <see cref="Person"/> по идентификаторам сотрудников
        /// </summary>
        Task<Dictionary<Guid, Person?>> GetPersonByEmployeeIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Проверка есть ли <see cref="Employee"/> по указанному id
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка <see cref="Employee"/> по id на категорию <see cref="EmployeeTypes.Instructor"/>
        /// </summary>
        Task<bool> AnyByIdWithInstructorAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Проверка <see cref="Employee"/> по id на категорию <see cref="EmployeeTypes.Student"/>
        /// </summary>
        Task<bool> AnyByIdWithStudentAsync(Guid id, CancellationToken cancellationToken);
    }
}
