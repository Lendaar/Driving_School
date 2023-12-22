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
        public PlaceModel Place { get; set; }

        /// <summary>
        /// Идентификатор обучающегося
        /// </summary>
        public PersonModel Student { get; set; }

        /// <summary>
        /// Инструктор
        /// </summary>
        public PersonModel Instructor { get; set; }

        /// <summary>
        /// Транспорт
        /// </summary>
        public TransportModel Transport { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public CourseModel Course { get; set; }
    }
}
