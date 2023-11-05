using Driving_School.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Driving_School.ContextConfiguration
{
    public class InstructorEntityTypeConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("TInstructors");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Experience).IsRequired();
            builder.Property(x => x.Number).IsRequired();
            builder.HasIndex(x => x.Experience)
                .HasFilter($"{nameof(Instructor.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Instructor)}_{nameof(Instructor.Experience)}");
            builder.HasMany(x => x.Lesson)
                .WithOne(x => x.Instructor)
                .HasForeignKey(x => x.InstructorId);
        }
    }
}