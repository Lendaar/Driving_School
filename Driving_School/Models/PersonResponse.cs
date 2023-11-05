namespace Driving_School.Api.Models
{
    /// <summary>
    /// Модель обущающегося
    /// </summary>
    public class PersonResponse
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
    }
}
