using Driving_School.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Driving_School.ContextConfiguration
{
    public class PlaceEntityTypeConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.ToTable("TPlaces");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.HasIndex(x => x.Name)
                .HasFilter($"{nameof(Place.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Place)}_{nameof(Place.Name)}");
            builder.HasMany(x => x.Lesson)
                .WithOne(x => x.Place)
                .HasForeignKey(x => x.PlaceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
