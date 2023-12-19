namespace Driving_School.Api.ModelsRequest.Course
{
    /// <summary>
    /// Модель запроса создания курса
    /// </summary>
    public class CreateCourseRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Продолжительность (Академических часов)
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Цена за занятие
        /// </summary>
        public decimal Price { get; set; }

    }
}
