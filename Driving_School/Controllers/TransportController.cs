using AutoMapper;
using Driving_School.Api.Attribute;
using Driving_School.Api.Infrastructures.Validator;
using Driving_School.Api.Models;
using Driving_School.Api.ModelsRequest.Transport;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Driving_School.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с Транспортом
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Transport")]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService transportService;
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        public TransportController(ITransportService transportService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.transportService = transportService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех Транспортных средств
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<TransportResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await transportService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<TransportResponse>>(result));
        }

        /// <summary>
        /// Получить Транспортное средство по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(TransportResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await transportService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти транспорт с идентификатором {id}");
            }
            return Ok(mapper.Map<TransportResponse>(item));
        }

        /// <summary>
        /// Создаёт новое Транспортное средство
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(TransportResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateTransportRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var transportRequestModel = mapper.Map<TransportRequestModel>(request);
            var result = await transportService.AddAsync(transportRequestModel, cancellationToken);
            return Ok(mapper.Map<TransportResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющееся Транспортное средство
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(TransportResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(TransportRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var model = mapper.Map<TransportRequestModel>(request);
            var result = await transportService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<TransportResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющееся Транспортное средство
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk(typeof(TransportResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await transportService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
