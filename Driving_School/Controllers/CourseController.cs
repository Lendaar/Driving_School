using AutoMapper;
using Driving_School.Api.Models;
using Driving_School.Api.ModelsRequest.Course;
using Driving_School.Api.ModelsRequest.Place;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.RequestModels;
using Driving_School.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await courseService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти курс с идентификатором {id}");
            }
            return Ok(mapper.Map<CourseResponse>(item));
        }

        /// <summary>
        /// Создаёт новый курс
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CourseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateCourseRequest request, CancellationToken cancellationToken)
        {
            var courseRequestModel = mapper.Map<CourseRequestModel>(request);
            var result = await courseService.AddAsync(courseRequestModel, cancellationToken);
            return Ok(mapper.Map<CourseResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющийся курс
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(CourseResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(CourseRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<CourseRequestModel>(request);
            var result = await courseService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<CourseResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийся курс
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await courseService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
