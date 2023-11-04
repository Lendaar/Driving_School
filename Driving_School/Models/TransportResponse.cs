using Driving_School.Api.Enums;

namespace Driving_School.Api.Models
{
    /// <summary>
    /// Модель транспорт
    /// </summary>
    public class TransportResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование транспорта
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Гос номер
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Тип КПП
        /// </summary>
        public string GSBType { get; set; } = string.Empty;
    }
}
