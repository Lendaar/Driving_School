namespace Driving_School.Services.Contracts.Models
{
    /// <summary>
    /// Модель инструктора
    /// </summary>
    public class InstructorModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
