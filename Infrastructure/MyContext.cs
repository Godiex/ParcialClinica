using Domain.Entities;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class MyContext : DbContextBase
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<CareStaff> CareStaffs { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Role> Roles { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration on creation
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Direction)
                .WithOne(i => i.Patient)
                .HasForeignKey<Patient>(p => p.IdDireccion);

            modelBuilder.Entity<CareStaff>()
                .HasOne(c => c.User)
                .WithOne(u => u.CareStaff)
                .HasForeignKey<CareStaff>(c => c.IdUser);

            modelBuilder.Entity<Quote>()
                .HasOne(q => q.Patient)
                .WithMany()
                .HasForeignKey(q => q.IdPatient);

        }
    }
}