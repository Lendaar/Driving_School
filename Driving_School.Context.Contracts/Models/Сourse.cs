namespace Driving_School.Context.Contracts.Models
{
    /// <summary>
    /// Сущность курса
    /// </summary>
    public class Course : BaseAuditEntity
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

        public ICollection<Lesson> Lesson { get; set; }

    }
}
