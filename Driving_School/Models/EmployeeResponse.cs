using Driving_School.Api.Enums;

namespace Driving_School.Api.Models
{
    /// <summary>
    /// Модель работника
    /// </summary>
    public class EmployeeResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }   

        /// <summary>
        /// ФИО
        /// </summary>
        public string FIO { get; set; } = string.Empty;

        /// <summary>
        /// Тип работника
        /// </summary>
        public string EmployeeType { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Стаж
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Внутренний номер
        /// </summary>
        public string Number { get; set; } = string.Empty;
    }
}