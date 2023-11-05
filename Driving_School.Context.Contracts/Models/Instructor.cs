namespace Driving_School.Context.Contracts.Models
{
    /// <summary>
    /// Сущность инструктора
    /// </summary>
    public class Instructor : BaseAuditEntity
    {
        /// <summary>
        /// Id person
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Сущность <see cref="Person"/>
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Стаж
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Внутренний номер
        /// </summary>
        public string Number { get; set; } = string.Empty;

        public ICollection<Lesson> Lesson { get; set; }
    }
}
