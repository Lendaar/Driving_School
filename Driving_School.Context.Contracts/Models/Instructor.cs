namespace Driving_School.Context.Contracts.Models
{
    /// <summary>
    /// Сущность инструктора
    /// </summary>
    public class Instructor : BaseAuditEntity
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
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string? Phone { get; set; } = string.Empty;

        /// <summary>
        /// Стаж
        /// </summary>
        public int? Experience { get; set; }
    }
}
