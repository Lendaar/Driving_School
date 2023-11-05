using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Driving_School.Api.Models;
using Driving_School.Services.Contracts.Interface;

namespace Driving_School.Api.Controllers
{
    /// <summary>
    /// CRUD контроллер по работу с Инструкторами]
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Instructor")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            this.instructorService = instructorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await instructorService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new InstructorResponse
            {
                //Id = x.Id,
                //LastName = x.LastName,
                //FirstName = x.FirstName,
                //Patronymic = x.Patronymic,
                //Phone = x.Phone,
                //Experience = x.Experience,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await instructorService.GetByIdAsync(id, cancellationToken);
            if (result == null)
            {
                return NotFound($"Не удалось найти инструктора с идентификатором {id}");
            }

            return Ok(new InstructorResponse
            {
                //Id = result.Id,
                //LastName = result.LastName,
                //FirstName = result.FirstName,
                //Patronymic = result.Patronymic,
                //Phone = result.Phone,
                //Experience = result.Experience,
            });
        }
    }
}
