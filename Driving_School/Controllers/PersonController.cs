using AutoMapper;
using Driving_School.Api.Attribute;
using Driving_School.Api.Models;
using Driving_School.Api.ModelsRequest.Person;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        [ApiOk(typeof(IEnumerable<PersonResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await personService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<PersonResponse>>(result));
        }

        /// <summary>
        /// Получить Персону по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(PersonResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await personService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти персону с идентификатором {id}");
            }
            return Ok(mapper.Map<PersonResponse>(item));
        }

        /// <summary>
        /// Создаёт новую Персону
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(PersonResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreatePersonRequest request, CancellationToken cancellationToken)
        {
            var personRequestModel = mapper.Map<PersonRequestModel>(request);
            var result = await personService.AddAsync(personRequestModel, cancellationToken);
            return Ok(mapper.Map<PersonResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющуюся Персону
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(PersonResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(PersonRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<PersonRequestModel>(request);
            var result = await personService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<PersonResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющуюся Персону
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk(typeof(PersonResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await personService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
