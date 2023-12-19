namespace Driving_School.Api.ModelsRequest.Lesson
{
    public class LessonRequest : CreateLessonRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
