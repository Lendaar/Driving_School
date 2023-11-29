namespace Driving_School.Services.Contracts.Exceptions
{
    /// <summary>
    /// Базовый класс исключений
    /// </summary>
    public abstract class Driving_SchoolException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Driving_SchoolException"/> без параметров
        /// </summary>
        protected Driving_SchoolException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Driving_SchoolException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected Driving_SchoolException(string message)
            : base(message) { }
    }
}
