namespace Driving_School.Api.ModelsRequest.Person
{
    public class PersonRequest : CreatePersonRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
