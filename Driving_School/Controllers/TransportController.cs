using AutoMapper;
using Driving_School.Api.Models;
using Driving_School.Api.ModelsRequest.Place;
using Driving_School.Api.ModelsRequest.Transport;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.RequestModels;
using Driving_School.Services.Implementations;
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
        private readonly IMapper mapper;

        public TransportController(ITransportService transportService, IMapper mapper)
        {
            this.transportService = transportService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех Транспортных средств
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransportResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await transportService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<TransportResponse>>(result));
        }

        /// <summary>
        /// Получить Транспортное средство по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<TransportResponse>), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(TransportResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateTransportRequest request, CancellationToken cancellationToken)
        {
            var transportRequestModel = mapper.Map<TransportRequestModel>(request);
            var result = await transportService.AddAsync(transportRequestModel, cancellationToken);
            return Ok(mapper.Map<TransportResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющееся Транспортное средство
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(TransportResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(TransportRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<TransportRequestModel>(request);
            var result = await transportService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<TransportResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющееся Транспортное средство
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await transportService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
