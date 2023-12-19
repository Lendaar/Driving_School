using AutoMapper;
using Driving_School.Api.Attribute;
using Driving_School.Api.Models;
using Driving_School.Api.ModelsRequest.Place;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        [ApiOk(typeof(IEnumerable<PlaceResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await placeService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<PlaceResponse>>(result));
        }

        /// <summary>
        /// Получить Площадку по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(PlaceResponse))]
        [ApiNotFound]
        public async Task<IActionResult> GetById([Required]Guid id, CancellationToken cancellationToken)
        {
            var item = await placeService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти площадку с идентификатором {id}");
            }
            return Ok(mapper.Map<PlaceResponse>(item));
        }

        /// <summary>
        /// Создаёт новую Площадку
        /// </summary>
        [HttpPost]
        [ApiOk(typeof(PlaceResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreatePlaceRequest request, CancellationToken cancellationToken)
        {
            var placeRequestModel = mapper.Map<PlaceRequestModel>(request);
            var result = await placeService.AddAsync(placeRequestModel, cancellationToken);
            return Ok(mapper.Map<PlaceResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющуюся Площадку
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(PlaceResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(PlaceRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<PlaceRequestModel>(request);
            var result = await placeService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<PlaceResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющуюся Площадку
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk(typeof(PlaceResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await placeService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
