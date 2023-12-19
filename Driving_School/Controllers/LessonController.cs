using Microsoft.AspNetCore.Mvc;
using Driving_School.Api.Models;
using Driving_School.Services.Contracts.Interface;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Driving_School.Api.ModelsRequest.Place;
using Driving_School.Services.Contracts.RequestModels;
using Driving_School.Services.Implementations;
using Driving_School.Api.ModelsRequest.Lesson;

namespace Driving_School.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с занятиями
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Lesson")]
    public class LessonController : Controller
    {
        private readonly ILessonService lessonService;
        private readonly IMapper mapper;

        public LessonController(ILessonService lessonService, IMapper mapper)
        {
            this.lessonService = lessonService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех занятий
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LessonResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await lessonService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<LessonResponse>>(result));
        }

        /// <summary>
        /// Получить занятие по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<LessonResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await lessonService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти занятие с идентификатором {id}");
            }
            return Ok(mapper.Map<LessonResponse>(item));
        }

        /// <summary>
        /// Создаёт новое занятие
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(LessonResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateLessonRequest request, CancellationToken cancellationToken)
        {
            var lessonRequestModel = mapper.Map<LessonRequestModel>(request);
            var result = await lessonService.AddAsync(lessonRequestModel, cancellationToken);
            return Ok(mapper.Map<LessonResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющееся занятие
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(LessonResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(LessonRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<LessonRequestModel>(request);
            var result = await lessonService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<LessonResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющееся занятие
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await lessonService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
