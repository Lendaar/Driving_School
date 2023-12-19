namespace Driving_School.Services.Contracts.Exceptions
{
    /// <summary>
    /// Ошибка выполнения операции
    /// </summary>
    public class Driving_SchoolInvalidOperationException : Driving_SchoolException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Driving_SchoolInvalidOperationException"/>
        /// с указанием сообщения об ошибке
        /// </summary>
        public Driving_SchoolInvalidOperationException(string message)
            : base(message)
        {

        }
    }
}
