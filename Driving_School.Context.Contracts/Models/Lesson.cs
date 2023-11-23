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
        /// Сущность площадки <see cref="Place"/>
        /// </summary>
        public Place Place { get; set; }

        /// <summary>
        /// Идентификатор обучающегося
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Сущность обучающегося <see cref="Employee"/>
        /// </summary>
        public Employee Student { get; set; }

        /// <summary>
        /// Идентификатор транспорта
        /// </summary>
        public Guid TransportId { get; set; }

        /// <summary>
        /// Сущность транспорта <see cref="Transport"/>
        /// </summary>
        public Transport Transport { get; set; }

        /// <summary>
        /// Инструктор
        /// </summary>
        public Guid InstructorId { get; set; }

        /// <summary>
        /// Сущность инструктора <see cref="Employee"/>
        /// </summary>
        public Employee Instructor { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public Guid CourceId { get; set; }

        /// <summary>
        /// Сущность курса <see cref="Course"/>
        /// </summary>
        public Course Cource { get; set; }
    }
}
