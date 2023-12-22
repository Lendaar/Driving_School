using AutoMapper;
using Driving_School.Api.Attribute;
using Driving_School.Api.Infrastructures.Validator;
using Driving_School.Api.Models;
using Driving_School.Api.ModelsRequest.Course;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.RequestModels;
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
        private readonly IApiValidatorService validatorService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CourseController"/>
        /// </summary>
        public CourseController(ICourseService courseService, IMapper mapper, IApiValidatorService validatorService)
        {
            this.courseService = courseService;
            this.validatorService = validatorService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список всех курсов
        /// </summary>
        [HttpGet]
        [ApiOk(typeof(IEnumerable<CourseResponse>))]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await courseService.GetAllAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<CourseResponse>>(result));
        }

        /// <summary>
        /// Получить курс по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ApiOk(typeof(CourseResponse))]
        [ApiNotFound]
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
        [ApiOk(typeof(CourseResponse))]
        [ApiConflict]
        public async Task<IActionResult> Create(CreateCourseRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var courseRequestModel = mapper.Map<CourseRequestModel>(request);
            var result = await courseService.AddAsync(courseRequestModel, cancellationToken);
            return Ok(mapper.Map<CourseResponse>(result));
        }

        /// <summary>
        /// Редактирует имеющийся курс
        /// </summary>
        [HttpPut]
        [ApiOk(typeof(CourseResponse))]
        [ApiNotFound]
        [ApiConflict]
        public async Task<IActionResult> Edit(CourseRequest request, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(request, cancellationToken);
            var model = mapper.Map<CourseRequestModel>(request);
            var result = await courseService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<CourseResponse>(result));
        }

        /// <summary>
        /// Удаляет имеющийся курс
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ApiOk(typeof(CourseResponse))]
        [ApiNotFound]
        [ApiNotAcceptable]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await courseService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
