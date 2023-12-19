using Driving_School.Api.Enums;

namespace Driving_School.Api.ModelsRequest.Transport
{
    /// <summary>
    /// Модель запроса создания транспорта
    /// </summary>
    public class CreateTransportRequest
    {
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
        public GSBTypes GSBType { get; set; } = GSBTypes.Mechanical;
    }
}
