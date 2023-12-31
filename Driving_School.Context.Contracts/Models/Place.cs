﻿namespace Driving_School.Context.Contracts.Models
{
    /// <summary>
    /// Сущность площадки
    /// </summary>
    public class Place : BaseAuditEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; } = string.Empty;

        public ICollection<Lesson> Lesson { get; set; }
    }
}
