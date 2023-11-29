using Driving_School.Services.Contracts.Enums;

namespace Driving_School.Services.Contracts.RequestModels
{
    /// <summary>
    /// Модель транспорт
    /// </summary>
    public class TransportRequestModel
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
        /// Тип документов
        /// </summary>
        public GSBTypes GSBType { get; set; }
    }
}
