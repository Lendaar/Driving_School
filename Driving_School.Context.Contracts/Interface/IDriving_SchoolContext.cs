using Driving_School.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Driving_School.Context.Contracts.Interface
{
    public interface IDriving_SchoolContext
    {
        /// <summary>Список <inheritdoc cref="Employee"/></summary>
        DbSet<Employee> Employees { get; }

        /// <summary>Список <inheritdoc cref="Lesson"/></summary>
        DbSet<Lesson> Lessons { get; }

        /// <summary>Список <inheritdoc cref="Place"/></summary>
        DbSet<Place> Places { get; }

        /// <summary>Список <inheritdoc cref="Person"/></summary>
        DbSet<Person> Persons { get; }

        /// <summary>Список <inheritdoc cref="Transport"/></summary>
        DbSet<Transport> Transports { get; }

        /// <summary>Список <inheritdoc cref="Course"/></summary>
        DbSet<Course> Courses { get; }
    }
}