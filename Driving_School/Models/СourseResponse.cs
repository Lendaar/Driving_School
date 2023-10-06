namespace Driving_School.Api.Models
{
    /// <summary>
    /// Модель курса
    /// </summary>
    public class CourseResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
