using Driving_School.Api.Enums;

namespace Driving_School.Api.ModelsRequest.Employee
{
    /// <summary>
    /// Модель запроса создания работника
    /// </summary>
    public class CreateEmployeeRequest
    {
        /// <summary>
        /// Идентификатор Person
        /// </summary>
        public Guid Person { get; set; }

        /// <summary>
        /// Тип работника
        /// </summary>
        public EmployeeTypes EmployeeType { get; set; } = EmployeeTypes.Student;

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
    }
}
