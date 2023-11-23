using Driving_School.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Driving_School.ContextConfiguration
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("TEmployees");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Experience).IsRequired();
            builder.Property(x => x.Number).IsRequired();
            builder.HasIndex(x => x.Experience)
                .HasFilter($"{nameof(Employee.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Employee)}_{nameof(Employee.Experience)}");
            builder.HasMany(x => x.LessonInstructor)
                .WithOne(x => x.Instructor)
                .HasForeignKey(x => x.InstructorId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.LessonStudent)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}