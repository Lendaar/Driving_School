using AutoMapper;
using Driving_School.Api.Models;
using Driving_School.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Driving_School.Api.Controllers
{

    /// <summary>
    /// CRUD контроллер по работу с Персонами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IMapper mapper;

        public PersonController(IPersonService personService, IMapper mapper)
        {
            this.personService = personService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех Персон
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await personService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<PersonResponse>>(result));
        }

        /// <summary>
        /// Получить Персону по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<PersonResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await personService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти персону с идентификатором {id}");
            }
            return Ok(mapper.Map<PersonResponse>(item));
        }
    }
}
