using Microsoft.AspNetCore.Mvc;
using Driving_School.Api.Models;
using Driving_School.Services.Contracts.Interface;
using AutoMapper;

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
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await lessonService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти занятие с идентификатором {id}");
            }
            return Ok(mapper.Map<LessonResponse>(item));
        }
    }
}
