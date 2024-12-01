using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cafe> Cafe  { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeCafe> EmployeeCafe  { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeCafe>()
                .HasKey(ec => new { ec.CafeId, ec.EmployeeId });

            modelBuilder.Entity<EmployeeCafe>()
                .HasOne(ec => ec.Cafe)
                .WithMany(c => c.EmployeeCafes)
                .HasForeignKey(ec => ec.CafeId);

            modelBuilder.Entity<EmployeeCafe>()
                .HasOne(ec => ec.Employee)
                .WithMany(e => e.EmployeeCafes)
                .HasForeignKey(ec => ec.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasCheckConstraint("CHK_PhoneNumber", "Phone_Number LIKE '[89]_______'")
                .HasCheckConstraint("CHK_Gender", "Gender IN ('Male', 'Female')");
        }
    }
}
