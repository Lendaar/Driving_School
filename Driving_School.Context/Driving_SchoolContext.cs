using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;

namespace Driving_School.Context
{
    public class Driving_SchoolContext : IDriving_SchoolContext
    {
        private readonly IList<Instructor> instructors;
        private readonly IList<Lesson> lessons;
        private readonly IList<Place> places;
        private readonly IList<Person> students;
        private readonly IList<Transport> transports;
        private readonly IList<Course> courses;

        public Driving_SchoolContext()
        {
            instructors = new List<Instructor>();
            lessons = new List<Lesson>();
            places = new List<Place>();
            students = new List<Person>();
            transports = new List<Transport>();
            courses = new List<Course>();
        }

        IEnumerable<Instructor> IDriving_SchoolContext.Instructors => instructors;

        IEnumerable<Lesson> IDriving_SchoolContext.Lessons => lessons;

        IEnumerable<Place> IDriving_SchoolContext.Places => places;

        IEnumerable<Person> IDriving_SchoolContext.Students => students;

        IEnumerable<Transport> IDriving_SchoolContext.Transports => transports;

        IEnumerable<Course> IDriving_SchoolContext.Courses => courses;
    }
}