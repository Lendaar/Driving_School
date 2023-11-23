using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Driving_School.Api.Models;
using Driving_School.Services.Contracts.Interface;

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
        private readonly IMapper mapper;

        public EmpoyeeController(IEmployeeService employeeService, IMapper mapper)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех Работников
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await employeeService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<EmployeeResponse>>(result));
        }

        /// <summary>
        /// Получить Работника по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти работника с идентификатором {id}");
            }
            return Ok(mapper.Map<EmployeeResponse>(item));
        }
    }
}
