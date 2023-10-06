using AutoMapper;
using Driving_School.Context.Contracts.Models;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Automappers
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Instructor, InstructorModel>(MemberList.Destination);

            CreateMap<Transport, TransportModel>(MemberList.Destination);

            CreateMap<Student, StudentModel>(MemberList.Destination);

            CreateMap<Place, PlaceModel>(MemberList.Destination);

            CreateMap<Course, CourseModel>(MemberList.Destination);

            CreateMap<Lesson, LessonModel>(MemberList.Destination)
                .ForMember(x => x.StartDate, next => next.MapFrom(e => e.StartDate))
                .ForMember(x => x.EndDate, next => next.MapFrom(e => e.EndDate))
                .ForMember(x => x.Instructor, next => next.MapFrom(e => new Instructor()))
                .ForMember(x => x.Place, next => next.MapFrom(e => new Place()))
                .ForMember(x => x.Student, next => next.MapFrom(e => new Student()))
                .ForMember(x => x.Transport, next => next.MapFrom(e => new Transport()))
                .ForMember(x => x.Course, next => next.MapFrom(e => new Course()));
            CreateMap<Transport, LessonModel>(MemberList.Destination)
                .ForMember(x => x.Transport, next => next.MapFrom(e => e));
            CreateMap<Instructor, LessonModel>(MemberList.Destination)
                .ForMember(x => x.Instructor, next => next.MapFrom(e => e));
            CreateMap<Place, LessonModel>(MemberList.Destination)
                .ForMember(x => x.Place, next => next.MapFrom(e => e));
            CreateMap<Student, LessonModel>(MemberList.Destination)
                .ForMember(x => x.Student, next => next.MapFrom(e => e));
            CreateMap<Course, LessonModel>(MemberList.Destination)
                .ForMember(x => x.Course, next => next.MapFrom(e => e));
        }
    }
}
