using Driving_School.Services.Contracts.Enums;

namespace Driving_School.Services.Contracts.RequestModels
{
    public class EmployeeRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
