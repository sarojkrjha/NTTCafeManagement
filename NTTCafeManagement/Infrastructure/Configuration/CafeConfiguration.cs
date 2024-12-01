using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class CafeConfiguration : IEntityTypeConfiguration<Cafe>
    {
        public void Configure(EntityTypeBuilder<Cafe> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Logo)
                .HasMaxLength(250);

            builder.HasMany(c => c.EmployeeCafes)
                .WithOne(e => e.Cafe)
                //.HasForeignKey(e => e.CafeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
