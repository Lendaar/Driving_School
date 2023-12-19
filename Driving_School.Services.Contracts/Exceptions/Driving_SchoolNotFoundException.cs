namespace Driving_School.Services.Contracts.Exceptions
{
    /// <summary>
    /// Запрашиваемый ресурс не найден
    /// </summary>
    public class Driving_SchoolNotFoundException : Driving_SchoolException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Driving_SchoolNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public Driving_SchoolNotFoundException(string message)
            : base(message)
        { }
    }
}
