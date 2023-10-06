namespace Driving_School.Api.Models
{
    /// <summary>
    /// Модель обущающегося
    /// </summary>
    public class StudentResponse
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
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirthday { get; set; }

        /// <summary>
        /// Пасспортные данные
        /// </summary>
        public string Passport { get; set; } = string.Empty;

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string? Email { get; set; } = string.Empty;

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; } = string.Empty;
    }
}
