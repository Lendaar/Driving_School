using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Driving_School.Api.Models;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Api.Controllers
{

    /// <summary>
    /// CRUD контроллер по работу с Обучающимися
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await studentService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new PersonResponse
            {
                //Id = x.Id,
                //LastName = x.LastName,
                //FirstName = x.FirstName,
                //Patronymic = x.Patronymic,
                //DateOfBirthday = x.DateOfBirthday,
                //Passport = x.Passport,
                //Email = x.Email ?? string.Empty,
                //Phone = x.Phone,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await studentService.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return NotFound($"Не удалось найти обучающегося с идентификатором {id}");
            }

            return Ok(new PersonResponse
            {
                //Id = item.Id,
                //LastName = item.LastName,
                //FirstName = item.FirstName,
                //Patronymic = item.Patronymic,
                //DateOfBirthday = item.DateOfBirthday,
                //Passport = item.Passport,
                //Email = item.Email ?? string.Empty,
                //Phone = item.Phone,
            });
        }
    }
}
