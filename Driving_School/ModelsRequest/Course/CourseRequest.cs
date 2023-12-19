namespace Driving_School.Api.ModelsRequest.Course
{
    public class CourseRequest : CreateCourseRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
