using Driving_School.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Driving_School.ContextConfiguration
{
    public class TransportEntityTypeConfiguration : IEntityTypeConfiguration<Transport>
    {
        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            builder.ToTable("TTransports");
            builder.HasKey(x => x.Id);
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.GSBType).IsRequired();
            builder.HasIndex(x => x.Name)
                .HasFilter($"{nameof(Transport.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Transport)}_{nameof(Transport.Name)}");
            builder.HasMany(x => x.Lesson)
                .WithOne(x => x.Transport)
                .HasForeignKey(x => x.TransportId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
