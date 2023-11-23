using AutoMapper;
using Driving_School.Api.Models;
using Driving_School.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await transportService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти транспорт с идентификатором {id}");
            }
            return Ok(mapper.Map<TransportResponse>(item));
        }
    }
}
