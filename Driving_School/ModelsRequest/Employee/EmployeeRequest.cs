namespace Driving_School.Api.ModelsRequest.Employee
{
    public class EmployeeRequest : CreateEmployeeRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
