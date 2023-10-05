namespace Driving_School.Services.Contracts.Models
{
    /// <summary>
    /// Модель занятия
    /// </summary>
    public class LessonModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        /// Инструктор
        /// </summary>
        public Guid InstructorId { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public Guid CourceId { get; set; }
    }
}
