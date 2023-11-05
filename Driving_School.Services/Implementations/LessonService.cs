using AutoMapper;
using Driving_School.Repositories.Contracts.Interface;
using Driving_School.Services.Anchors;
using Driving_School.Services.Contracts.Interface;
using Driving_School.Services.Contracts.Models;

namespace Driving_School.Services.Implementations
{
    public class LessonService : ILessonService, IServiceAnchor
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
            var transportId = lessons.Select(x => x.TransportId).Distinct();
            var instructorId = lessons.Select(x => x.InstructorId).Distinct();
            var studentId = lessons.Select(x => x.PersonId).Distinct();
            var courseId = lessons.Select(x => x.CourceId).Distinct();
            var placeId = lessons.Select(x => x.PlaceId).Distinct();

            var transports = await transportReadRepository.GetByIdsAsync(transportId, cancellationToken);
            var instructors = await instructorReadRepository.GetByIdsAsync(instructorId, cancellationToken);
            var students = await studentReadRepository.GetByIdsAsync(studentId, cancellationToken);
            var courses = await courseReadRepository.GetByIdsAsync(courseId, cancellationToken);
            var places = await placeReadRepository.GetByIdsAsync(placeId, cancellationToken);

            var listLessonModel = new List<LessonModel>();
            foreach (var lessonItem in lessons)
            {
                if (!transports.TryGetValue(lessonItem.PlaceId, out var transport)) continue;
                if (!instructors.TryGetValue(lessonItem.PlaceId, out var instructor)) continue;
                if (!students.TryGetValue(lessonItem.PlaceId, out var student)) continue;
                if (!courses.TryGetValue(lessonItem.CourceId, out var course)) continue;
                if (!places.TryGetValue(lessonItem.PlaceId, out var place)) continue;

                var lessonTable = mapper.Map<LessonModel>(lessonItem);

                lessonTable.Transport = mapper.Map<TransportModel>(transport);
                lessonTable.Instructor = mapper.Map<InstructorModel>(instructor);
                lessonTable.Student = mapper.Map<PersonModel>(student);
                lessonTable.Course = mapper.Map<CourseModel>(course);
                lessonTable.Place = mapper.Map<PlaceModel>(place);
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
            var student = await studentReadRepository.GetByIdAsync(item.PersonId, cancellationToken);
            var course = await courseReadRepository.GetByIdAsync(item.CourceId, cancellationToken);
            var place = await placeReadRepository.GetByIdAsync(item.PlaceId, cancellationToken);

            var lesson = mapper.Map<LessonModel>(item);

            lesson.Transport = mapper.Map<TransportModel>(transport);
            lesson.Student = mapper.Map<PersonModel>(student);
            lesson.Place = mapper.Map<PlaceModel>(place);
            lesson.Course = mapper.Map<CourseModel>(course);
            lesson.Instructor = mapper.Map<InstructorModel>(instructor);
            return lesson;
        }
    }
}
