using Driving_School.Context.Contracts.Models;

namespace Driving_School.Context.Contracts.Interface
{
    public interface IDriving_SchoolContext
    {
        IEnumerable<Instructor> Instructors { get; }
        IEnumerable<Lesson> Lessons { get; }
        IEnumerable<Place> Places { get; }
        IEnumerable<Person> Students { get; }
        IEnumerable<Transport> Transports { get; }
        IEnumerable<Course> Courses { get; }
    }
}