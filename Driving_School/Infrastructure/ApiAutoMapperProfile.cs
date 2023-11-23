using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Driving_School.Api.Models;
using GSBTypes_Services = Driving_School.Services.Contracts.Enums.GSBTypes;
using GSBTypes_Api = Driving_School.Api.Enums.GSBTypes;
using EmployeeTypes_Services = Driving_School.Services.Contracts.Enums.EmployeeTypes;
using EmployeeTypes_Api = Driving_School.Api.Enums.EmployeeTypes;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Api.Infrastructure
{
    public class ApiAutoMapperProfile : Profile
    {
        public ApiAutoMapperProfile()
        {
            CreateMap<CourseModel, CourseResponse>(MemberList.Destination);
            CreateMap<TransportModel, TransportResponse>(MemberList.Destination);
            CreateMap<PlaceModel, PlaceResponse>(MemberList.Destination);
            CreateMap<PersonModel, PersonResponse>(MemberList.Destination);

            CreateMap<EmployeeModel, EmployeeResponse>(MemberList.Destination)
            .ForMember(x => x.FIO, opt => opt.MapFrom(x => $"{x.Person.LastName} {x.Person.FirstName} {x.Person.Patronymic}"))
            .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Person.Phone));

            CreateMap<LessonModel, LessonResponse>(MemberList.Destination)
            .ForMember(x => x.InstructorName, opt => opt.MapFrom(x => $"{x.Instructor.Person.LastName} {x.Instructor.Person.FirstName} {x.Instructor.Person.Patronymic}"))
            .ForMember(x => x.StudentName, opt => opt.MapFrom(x => $"{x.Student.Person.LastName} {x.Student.Person.FirstName} {x.Student.Person.Patronymic}"))
            .ForMember(x => x.PlaceName, opt => opt.MapFrom(x => x.Place.Name))
            .ForMember(x => x.TransportName, opt => opt.MapFrom(x => x.Transport.Name))
            .ForMember(x => x.CourseName, opt => opt.MapFrom(x => x.Course.Name));

            CreateMap<GSBTypes_Services, GSBTypes_Api>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<EmployeeTypes_Services, EmployeeTypes_Api>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();
        }
    }
}
