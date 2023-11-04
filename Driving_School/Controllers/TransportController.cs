using Driving_School.Api.Models;
using Driving_School.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

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

        public TransportController(ITransportService transportService)
        {
            this.transportService = transportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await transportService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new TransportResponse
            {
                Id = x.Id,
                Name = x.Name,
                Number = x.Number,
                GSBType = x.GSBType.GetDisplayName(),
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await transportService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти транспорт с идентификатором {id}");
            }

            return Ok(new TransportResponse
            {
                Id = result.Id,
                Name = result.Name,
                Number = result.Number,
                GSBType = result.GSBType.GetDisplayName(),
            });
        }
    }
}
