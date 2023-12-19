using AutoMapper;
using Driving_School.Common.Entity.InterfaceDB;
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
    public class PersonService : IPersonService, IServiceAnchor
    {
        private readonly IPersonReadRepository personReadRepository;
        private readonly IPersonWriteRepository personWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PersonService(IPersonReadRepository personReadRepository, 
            IMapper mapper,
            IPersonWriteRepository personWriteRepository,
            IUnitOfWork unitOfWork)
        {
            this.personReadRepository = personReadRepository;
            this.personWriteRepository = personWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<PersonModel>> IPersonService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await personReadRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<PersonModel>>(result);
        }

        async Task<PersonModel?> IPersonService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await personReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }

            return mapper.Map<PersonModel>(item);
        }

        async Task<PersonModel> IPersonService.AddAsync(PersonRequestModel person, CancellationToken cancellationToken)
        {
            var item = new Person
            {
                Id = Guid.NewGuid(),
                LastName = person.LastName,
                FirstName = person.FirstName,
                Patronymic = person.Patronymic,
                DateOfBirthday = person.DateOfBirthday,
                Passport = person.Passport,
                Phone = person.Phone,
            };
            personWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PersonModel>(item);
        }

        async Task<PersonModel> IPersonService.EditAsync(PersonRequestModel source, CancellationToken cancellationToken)
        {
            var targetPlace = await personReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetPlace == null)
            {
                throw new Driving_SchoolEntityNotFoundException<Person>(source.Id);
            }

            targetPlace.LastName = source.LastName;
            targetPlace.FirstName = source.FirstName;
            targetPlace.Patronymic = source.Patronymic;
            targetPlace.DateOfBirthday = source.DateOfBirthday;
            targetPlace.Passport = source.Passport;
            targetPlace.Phone = source.Phone;

            personWriteRepository.Update(targetPlace);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<PersonModel>(targetPlace);
        }

        async Task IPersonService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetPlace = await personReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetPlace == null)
            {
                throw new Driving_SchoolEntityNotFoundException<Person>(id);
            }

            if (targetPlace.DeletedAt.HasValue)
            {
                throw new Driving_SchoolInvalidOperationException($"Персона с идентификатором {id} уже удалена");
            }

            personWriteRepository.Delete(targetPlace);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
