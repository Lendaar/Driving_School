using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Driving_School.Api.Models;
using GSBTypes_Services = Driving_School.Services.Contracts.Enums.GSBTypes;
using GSBTypes_Api = Driving_School.Api.Enums.GSBTypes;
using EmployeeTypes_Services = Driving_School.Services.Contracts.Enums.EmployeeTypes;
using EmployeeTypes_Api = Driving_School.Api.Enums.EmployeeTypes;
using Driving_School.Services.Contracts.Models;
using Driving_School.Api.ModelsRequest.Lesson;
using Driving_School.Services.Contracts.RequestModels;
using Driving_School.Api.ModelsRequest.Employee;
using Driving_School.Api.ModelsRequest.Course;
using Driving_School.Api.ModelsRequest.Transport;
using Driving_School.Api.ModelsRequest.Place;
using Driving_School.Api.ModelsRequest.Person;

namespace Driving_School.Api.Infrastructure
{
    public class ApiAutoMapperProfile : Profile
    {
        public ApiAutoMapperProfile()
        {
            CreateMap<CourseModel, CourseResponse>(MemberList.Destination);
            CreateMap<CreateCourseRequest, CourseRequestModel>(MemberList.Destination);
            CreateMap<CourseRequest, CourseRequestModel>(MemberList.Destination);

            CreateMap<TransportModel, TransportResponse>(MemberList.Destination);
            CreateMap<CreateTransportRequest, TransportRequestModel>(MemberList.Destination);
            CreateMap<TransportRequest, TransportRequestModel>(MemberList.Destination);

            CreateMap<PlaceModel, PlaceResponse>(MemberList.Destination);
            CreateMap<CreatePlaceRequest, PlaceRequestModel>(MemberList.Destination);
            CreateMap<PlaceRequest, PlaceRequestModel>(MemberList.Destination);

            CreateMap<PersonModel, PersonResponse>(MemberList.Destination)
                .ForMember(x => x.FIO, opt => opt.MapFrom(x => $"{x.LastName} {x.FirstName} {x.Patronymic ?? string.Empty}"));
            CreateMap<CreatePersonRequest, PersonRequestModel>(MemberList.Destination);
            CreateMap<PersonRequest, PersonRequestModel>(MemberList.Destination);

            CreateMap<EmployeeModel, EmployeeResponse>(MemberList.Destination)
                .ForMember(x => x.FIO, opt => opt.MapFrom(x => x.Person != null
                    ? $"{x.Person.LastName} {x.Person.FirstName} {x.Person.Patronymic}"
                    : string.Empty))
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Person != null
                    ? x.Person.Phone
                    : string.Empty));

            CreateMap<CreateEmployeeRequest, EmployeeRequestModel>(MemberList.Destination);
            CreateMap<EmployeeRequest, EmployeeRequestModel>(MemberList.Destination);

            CreateMap<LessonModel, LessonResponse>(MemberList.Destination)
            .ForMember(x => x.InstructorName, opt => opt.MapFrom(x => $"{x.Instructor.LastName} {x.Instructor.FirstName} {x.Instructor.Patronymic ?? string.Empty}"))
            .ForMember(x => x.StudentName, opt => opt.MapFrom(x => $"{x.Student.LastName} {x.Student.FirstName} {x.Student.Patronymic ?? string.Empty}"))
            .ForMember(x => x.PlaceName, opt => opt.MapFrom(x => x.Place.Name))
            .ForMember(x => x.TransportName, opt => opt.MapFrom(x => x.Transport.Name))
            .ForMember(x => x.CourseName, opt => opt.MapFrom(x => x.Course.Name));

            CreateMap<CreateLessonRequest, LessonRequestModel>(MemberList.Destination);
            CreateMap<LessonRequest, LessonRequestModel>(MemberList.Destination);

            CreateMap<GSBTypes_Services, GSBTypes_Api>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();

            CreateMap<EmployeeTypes_Services, EmployeeTypes_Api>()
                .ConvertUsingEnumMapping(opt => opt.MapByName())
                .ReverseMap();
        }
    }
}
