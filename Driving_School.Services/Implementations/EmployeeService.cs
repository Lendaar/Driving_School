using AutoMapper;
using Driving_School.Common.Entity.InterfaceDB;
using Driving_School.Context.Contracts.Enums;
using Driving_School.Context.Contracts.Models;
using Driving_School.Repositories.Contracts;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Anchors;
using Driving_School.Services.Contracts.Exceptions;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;
using Driving_School.Services.Contracts.RequestModels;

namespace Driving_School.Services.Implementations
{
    public class EmployeeService : IEmployeeService, IServiceAnchor
    {
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IEmployeeWriteRepository employeeWriteRepository;
        private readonly IPersonReadRepository personReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeReadRepository employeeReadRepository,
            IMapper mapper,
            IEmployeeWriteRepository employeeWriteRepository,
            IPersonReadRepository personReadRepository,
            IUnitOfWork unitOfWork)
        {
            this.employeeReadRepository = employeeReadRepository;
            this.personReadRepository = personReadRepository;
            this.employeeWriteRepository = employeeWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<EmployeeModel>> IEmployeeService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await employeeReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<EmployeeModel>>(result);
        }

        async Task<EmployeeModel?> IEmployeeService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            
            return mapper.Map<EmployeeModel>(item);
        }

        async Task<EmployeeModel> IEmployeeService.AddAsync(EmployeeRequestModel employeeRequestModel, CancellationToken cancellationToken)
        {
            var item = new Employee
            {
                Id = Guid.NewGuid(),
                EmployeeType = (EmployeeTypes)employeeRequestModel.EmployeeType,
                PersonId = employeeRequestModel.Person,
                Email = employeeRequestModel.Email,
                Experience = employeeRequestModel.Experience,
                Number = employeeRequestModel.Number   
            };

            employeeWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<EmployeeModel>(item);
        }
        async Task<EmployeeModel> IEmployeeService.EditAsync(EmployeeRequestModel source, CancellationToken cancellationToken)
        {
            var targetEmployee = await employeeReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetEmployee == null)
            {
                throw new Driving_SchoolEntityNotFoundException<Employee>(source.Id);
            }

            targetEmployee.EmployeeType = (EmployeeTypes)source.EmployeeType;
            targetEmployee.Email = source.Email;
            targetEmployee.Experience = source.Experience;
            targetEmployee.Number = source.Number;

            var person = await personReadRepository.GetByIdAsync(source.Person, cancellationToken);
            targetEmployee.PersonId = person!.Id;
            targetEmployee.Person = person;

            employeeWriteRepository.Update(targetEmployee);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<EmployeeModel>(targetEmployee);
        }
        async Task IEmployeeService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetEmployee = await employeeReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetEmployee == null)
            {
                throw new Driving_SchoolEntityNotFoundException<Employee>(id);
            }
            if (targetEmployee.DeletedAt.HasValue)
            {
                throw new Driving_SchoolInvalidOperationException($"Рабочий с идентификатором {id} уже удален");
            }

            employeeWriteRepository.Delete(targetEmployee);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
