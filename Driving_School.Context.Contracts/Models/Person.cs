namespace Driving_School.Context.Contracts.Models
{
    /// <summary>
    /// Сущность персоны (студента)
    /// </summary>
    public class Person : BaseAuditEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirthday { get; set; }

        /// <summary>
        /// Пасспортные данные
        /// </summary>
        public string Passport { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        public ICollection<Employee> Employee { get; set; }
    }
}
