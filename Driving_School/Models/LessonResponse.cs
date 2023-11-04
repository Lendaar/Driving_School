namespace Driving_School.Api.Models
{
    /// <summary>
    /// Модель занятия
    /// </summary>
    public class LessonResponse
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
        /// Наименование площадки
        /// </summary>
        public string PlaceName { get; set; } = string.Empty;

        /// <summary>
        /// ФИО обучающегося
        /// </summary>
        public string StudentName { get; set; } = string.Empty;

        /// <summary>
        /// Инструктор
        /// </summary>
        public string InstructorName { get; set; } = string.Empty;

        /// <summary>
        /// Транспорт
        /// </summary>
        public string TransportName { get; set; } = string.Empty;

        /// <summary>
        /// Курс
        /// </summary>
        public string CourseName { get; set; } = string.Empty;
    }
}
