using Driving_School.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Driving_School.ContextConfiguration
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("TPersons");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.DateOfBirthday).IsRequired();
            builder.Property(x => x.Passport).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.HasIndex(x => x.LastName)
                    .HasFilter($"{nameof(Person.DeletedAt)} is null")
                    .HasDatabaseName($"IX_{nameof(Person)}_{nameof(Person.LastName)}");
            builder.HasMany(x => x.Lesson)
                    .WithOne(x => x.Person)
                    .HasForeignKey(x => x.PersonId);
        }
    }
}
