namespace Driving_School.Api.ModelsRequest.Lesson
{
    /// <summary>
    /// Модель запроса создания занятия
    /// </summary>
    public class CreateLessonRequest
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
        public Guid Place { get; set; }

        /// <summary>
        /// Идентификатор обучающегося
        /// </summary>
        public Guid Student { get; set; }

        /// <summary>
        /// Инструктор
        /// </summary>
        public Guid Instructor { get; set; }

        /// <summary>
        /// Транспорт
        /// </summary>
        public Guid Transport { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public Guid Course { get; set; }
    }
}
