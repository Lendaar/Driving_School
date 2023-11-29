﻿using Driving_School.Services.Contracts.Enums;
using Driving_School.Services.Contracts.Models;

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
        public PersonModel Person { get; set; }

        /// <summary>
        /// Тип работника
        /// </summary>
        public EmployeeTypes EmployeeType { get; set; }

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
