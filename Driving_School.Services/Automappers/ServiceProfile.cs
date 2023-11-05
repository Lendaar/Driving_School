using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Driving_School.Context.Contracts.Models;
using Driving_School.Services.Contracts.Models;
using GSBTypes_Contracts = Driving_School.Context.Contracts.Enums.GSBTypes;
using GSBTypes_Services = Driving_School.Services.Contracts.Enums.GSBTypes;

namespace Driving_School.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Instructor, InstructorModel>(MemberList.Destination);

            CreateMap<Transport, TransportModel>(MemberList.Destination);

            CreateMap<Person, PersonModel>(MemberList.Destination);

            CreateMap<Place, PlaceModel>(MemberList.Destination);

            CreateMap<Course, CourseModel>(MemberList.Destination);

            CreateMap<Lesson, LessonModel>(MemberList.Destination)
                .ForMember(x => x.Instructor, next => next.Ignore())
                .ForMember(x => x.Place, next => next.Ignore())
                .ForMember(x => x.Student, next => next.Ignore())
                .ForMember(x => x.Transport, next => next.Ignore())
                .ForMember(x => x.Course, next => next.Ignore());

            CreateMap<GSBTypes_Contracts, GSBTypes_Services>().ConvertUsingEnumMapping(x => x.MapByName()).ReverseMap();
        }
    }
}
