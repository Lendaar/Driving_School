using AutoMapper;
using Driving_School.Api.Models;
using Driving_School.Services.Contracts.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Driving_School.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с Курсами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Course")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService; 
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CourseController"/>
        /// </summary>
        public CourseController(ICourseService courseService, IMapper mapper)
        {
            this.courseService = courseService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех курсов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await courseService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<CourseResponse>>(result));
        }

        /// <summary>
        /// Получить курс по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<CourseResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await courseService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти курс с идентификатором {id}");
            }
            return Ok(mapper.Map<CourseResponse>(item));
        }
    }
}
