using Driving_School.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Driving_School.ContextConfiguration
{
    public class LessonEntityTypeConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("TLessons");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.HasIndex(x => x.StartDate)
                .HasFilter($"{nameof(Lesson.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Lesson)}_{nameof(Lesson.StartDate)}");
        }
    }
}
