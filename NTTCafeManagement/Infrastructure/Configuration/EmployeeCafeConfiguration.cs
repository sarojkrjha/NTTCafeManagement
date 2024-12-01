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
    public class EmployeeCafeConfiguration : IEntityTypeConfiguration<EmployeeCafe>
    {
        public void Configure(EntityTypeBuilder<EmployeeCafe> builder)
        {
            builder.HasKey(ec => new { ec.EmployeeId, ec.CafeId });

            builder.HasOne(ec => ec.Employee)
                .WithMany()
                .HasForeignKey(ec => ec.EmployeeId);

            builder.HasOne(ec => ec.Cafe)
                .WithMany()
                .HasForeignKey(ec => ec.CafeId);

            builder.Property(ec => ec.StartDate)
                .IsRequired();
        }
    }
}
