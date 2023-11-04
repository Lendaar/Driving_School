namespace Driving_School.Context.Contracts.Models
{
    /// <summary>
    /// Сущность занятия
    /// </summary>
    public class Lesson: BaseAuditEntity
    {
        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// Идентификатор площадки
        /// </summary>
        public Guid PlaceId { get; set; }

        /// <summary>
        /// Идентификатор обучающегося
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Идентификатор транспорта
        /// </summary>
        public Guid TransportId { get; set; }

        /// <summary>
        /// Инструктор
        /// </summary>
        public Guid InstructorId { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public Guid CourceId { get; set; }
    }
}
