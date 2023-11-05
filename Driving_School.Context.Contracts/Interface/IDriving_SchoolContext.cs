using Driving_School.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Driving_School.Context.Contracts.Interface
{
    public interface IDriving_SchoolContext
    {
        DbSet<Instructor> Instructors { get; }
        DbSet<Lesson> Lessons { get; }
        DbSet<Place> Places { get; }
        DbSet<Person> Students { get; }
        DbSet<Transport> Transports { get; }
        DbSet<Course> Courses { get; }
    }
}