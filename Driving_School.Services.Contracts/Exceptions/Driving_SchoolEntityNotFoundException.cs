namespace Driving_School.Services.Contracts.Exceptions
{
    /// <summary>
    /// Запрашиваемая сущность не найдена
    /// </summary>
    public class Driving_SchoolEntityNotFoundException<TEntity> : Driving_SchoolNotFoundException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Driving_SchoolEntityNotFoundException{TEntity}"/>
        /// </summary>
        public Driving_SchoolEntityNotFoundException(Guid id)
            : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}
