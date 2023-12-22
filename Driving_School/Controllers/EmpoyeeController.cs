using AutoMapper;
using Driving_School.Api.Attribute;
using Driving_School.Api.Infrastructures.Validator;
using Driving_School.Api.Models;
using Driving_School.Api.ModelsRequest.Employee;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Driving_School.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с Работниками
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Employee")]
    public class EmpoyeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        public EmpoyeeController(IEmployeeService employeeService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.employeeService = employeeService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех Работников
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<EmployeeResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await employeeService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<EmployeeResponse>>(result));
        }

        /// <summary>
        /// Получить Работника по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(EmployeeResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required]Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти работника с идентификатором {id}");
            }
            return Ok(mapper.Map<EmployeeResponse>(item));
        }

        /// <summary>
        /// Создаёт нового рабочего
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(EmployeeResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var employeeRequestModel = mapper.Map<EmployeeRequestModel>(request);
            var result = await employeeService.AddAsync(employeeRequestModel, cancellationToken);
            return Ok(mapper.Map<EmployeeResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющищегося рабочего
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(EmployeeResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(EmployeeRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var model = mapper.Map<EmployeeRequestModel>(request);
            var result = await employeeService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<EmployeeResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийегося рабочего по id
        /// </summary>
        [HttpDelete("{id}")]
        [ApiOk(typeof(EmployeeResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await employeeService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
