using AutoMapper;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Implementations
{
    public class LessonService : ILessonService
    {
        private readonly ILessonReadRepository lessonReadRepository;
        private readonly ICourseReadRepository courseReadRepository;
        private readonly ITransportReadRepository transportReadRepository;
        private readonly IInstructorReadRepository instructorReadRepository;
        private readonly IStudentReadRepository studentReadRepository;
        private readonly IPlaceReadRepository placeReadRepository;
        private readonly IMapper mapper;

        public LessonService(ILessonReadRepository lessonReadRepository,
            ICourseReadRepository courseReadRepository,
            ITransportReadRepository transportReadRepository,
            IInstructorReadRepository instructorReadRepository,
            IStudentReadRepository studentReadRepository,
            IPlaceReadRepository placeReadRepository,
            IMapper mapper)
        {
            this.lessonReadRepository = lessonReadRepository;
            this.courseReadRepository = courseReadRepository;
            this.transportReadRepository = transportReadRepository;
            this.instructorReadRepository = instructorReadRepository;
            this.studentReadRepository = studentReadRepository;
            this.placeReadRepository = placeReadRepository;
            this.mapper = mapper;
        }
        
        async Task<IEnumerable<LessonModel>> ILessonService.GetAllAsync(CancellationToken cancellationToken)
        {
            var lessons = await lessonReadRepository.GetAllAsync(cancellationToken);
            IEnumerable<Guid> transportId, instructorId, studentId, courseId, placeId;
            transportId = lessons.Select(x => x.TransportId).Distinct().Cast<Guid>();
            instructorId = lessons.Select(x => x.InstructorId).Distinct().Cast<Guid>();
            studentId = lessons.Select(x => x.StudentId).Distinct().Cast<Guid>();
            courseId = lessons.Select(x => x.CourceId).Distinct().Cast<Guid>();
            placeId = lessons.Select(x => x.PlaceId).Distinct().Cast<Guid>();

            var transports = await transportReadRepository.GetByIdsAsync(transportId, cancellationToken);
            var instructors = await instructorReadRepository.GetByIdsAsync(instructorId, cancellationToken);
            var students = await studentReadRepository.GetByIdsAsync(studentId, cancellationToken);
            var courses = await courseReadRepository.GetByIdsAsync(courseId, cancellationToken);
            var places = await placeReadRepository.GetByIdsAsync(placeId, cancellationToken);

            var listLessonModel = new List<LessonModel>();
            foreach (var lessonItem in lessons)
            {
                var transport = transports.FirstOrDefault(x => x.Id == lessonItem.TransportId);
                var instructor = instructors.FirstOrDefault(x => x.Id == lessonItem.InstructorId);
                var student = students.FirstOrDefault(x => x.Id == lessonItem.StudentId);
                var course = courses.FirstOrDefault(x => x.Id == lessonItem.CourceId);
                var place = places.FirstOrDefault(x => x.Id == lessonItem.PlaceId);

                var lessonTable = mapper.Map<LessonModel>(lessonItem);
                var lessonItemTransport = mapper.Map<LessonModel>(transport);
                var lessonItemInstructor = mapper.Map<LessonModel>(instructor);
                var lessonItemStudent = mapper.Map<LessonModel>(student);
                var lessonTableCource = mapper.Map<LessonModel>(course);
                var lessonTablePlace = mapper.Map<LessonModel>(place);

                lessonTable.Transport = lessonItemTransport.Transport;
                lessonTable.Instructor = lessonItemInstructor.Instructor;
                lessonTable.Student = lessonItemStudent.Student;
                lessonTable.Course = lessonTableCource.Course;
                lessonTable.Place = lessonTableCource.Place;
                listLessonModel.Add(lessonTable);
            }

            return listLessonModel;
        }

        async Task<LessonModel?> ILessonService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await lessonReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                return null;
            }
            var transport = await transportReadRepository.GetByIdAsync(item.TransportId, cancellationToken);
            var instructor = await instructorReadRepository.GetByIdAsync(item.InstructorId, cancellationToken);
            var student = await studentReadRepository.GetByIdAsync(item.StudentId, cancellationToken);
            var course = await courseReadRepository.GetByIdAsync(item.CourceId, cancellationToken);
            var place = await placeReadRepository.GetByIdAsync(item.PlaceId, cancellationToken);

            var lesson = mapper.Map<LessonModel>(item);
            var lessonTransport = mapper.Map<LessonModel>(transport);
            var lessonInstructort = mapper.Map<LessonModel>(instructor);
            var lessonStudent = mapper.Map<LessonModel>(student);
            var lessonCource = mapper.Map<LessonModel>(course);
            var lessonPlace = mapper.Map<LessonModel>(place);

            lesson.Transport = lessonTransport.Transport;
            lesson.Student = lessonStudent.Student;
            lesson.Place = lessonPlace.Place;
            lesson.Course = lessonCource.Course;
            lesson.Instructor = lessonInstructort.Instructor;
            return lesson;
        }
    }
}
