using Driving_School.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Driving_School.ContextConfiguration
{
    public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("TCourses");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Duration).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.HasIndex(x => x.Name)
                .HasFilter($"{nameof(Course.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Course)}_{nameof(Course.Name)}");
            builder.HasMany(x => x.Lesson)
                .WithOne(x => x.Cource)
                .HasForeignKey(x => x.CourceId);
        }
    }
}
