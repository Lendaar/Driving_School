using Driving_School.Context.Contracts.Enums;

namespace Driving_School.Context.Contracts.Models
{
    /// <summary>
    /// Сущность транспорта
    /// </summary>
    public class Transport: BaseAuditEntity
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
        public GSBTypes GSBType { get; set; }

        public ICollection<Lesson> Lesson { get; set; }
    }
}
