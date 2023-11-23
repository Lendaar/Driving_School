using Microsoft.AspNetCore.Mvc;
using Driving_School.Api.Models;
using Driving_School.Services.Contracts.Interface;
using AutoMapper;

namespace Driving_School.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с Площадкой
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Place")]
    public class PlaceController : Controller
    {
        private readonly IPlaceService placeService;
        private readonly IMapper mapper;

        public PlaceController(IPlaceService placeService, IMapper mapper)
        {
            this.placeService = placeService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех Площадок
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlaceResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await placeService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<PlaceResponse>>(result));
        }

        /// <summary>
        /// Получить Площадку по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<PlaceResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await placeService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти площадку с идентификатором {id}");
            }
            return Ok(mapper.Map<PlaceResponse>(item));
        }
    }
}
