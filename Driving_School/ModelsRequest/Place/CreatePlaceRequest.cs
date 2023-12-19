namespace Driving_School.Api.ModelsRequest.Place
{
    /// <summary>
    /// Модель запроса создания площадки
    /// </summary>
    public class CreatePlaceRequest
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
    }
}
