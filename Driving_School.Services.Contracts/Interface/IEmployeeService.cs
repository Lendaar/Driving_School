﻿using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Contracts.Interface
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Получить список всех <see cref="EmployeeModel"/>
        /// </summary>
        Task<IEnumerable<EmployeeModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="EmployeeModel"/> по идентификатору
        /// </summary>
        Task<EmployeeModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
