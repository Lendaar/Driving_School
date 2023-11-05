using Driving_School.Context.Contracts.Interface;
using Driving_School.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Driving_School.Context
{
    public class Driving_SchoolContext : DbContext, IDriving_SchoolContext
    {
        public Driving_SchoolContext(DbContextOptions<Driving_SchoolContext> options) : base (options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {

        }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Person> Students { get; set; }

        public DbSet<Transport> Transports { get; set; }

        public DbSet<Course> Courses { get; set; }
    }
}