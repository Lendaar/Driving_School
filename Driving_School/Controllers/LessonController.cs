using Microsoft.AspNetCore.Mvc;
using Driving_School.Api.Models;
using Driving_School.Services.Contracts.Interface;

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

        public LessonController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await lessonService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new LessonResponse
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                PlaceName = x.Place?.Name ?? string.Empty,
                StudentName = $"{x.Student?.LastName} {x.Student?.FirstName} {x.Student?.Patronymic}",
                InstructorName = $"{x.Instructor?.LastName} {x.Instructor?.FirstName} {x.Instructor?.Patronymic}",
                TransportName = x.Transport?.Name ?? string.Empty,
                CourseName = x.Course?.Name ?? string.Empty
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await lessonService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти занятие с идентификатором {id}");
            }

            return Ok(new LessonResponse
            {
                Id = item.Id,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                PlaceName = item.Place?.Name ?? string.Empty,
                StudentName = $"{item.Student?.LastName} {item.Student?.FirstName} {item.Student?.Patronymic}",
                InstructorName = $"{item.Instructor?.LastName} {item.Instructor?.FirstName} {item.Instructor?.Patronymic}",
                TransportName = item.Transport?.Name ?? string.Empty,
                CourseName = item.Course?.Name ?? string.Empty
            });
        }
    }
}
