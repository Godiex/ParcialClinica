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
        public DbSet<QuoteCareStaff> QuoteCareStaffs { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration on creation
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quote>()
                .HasMany(c => c.CareStaff)
                .WithMany(q => q.Quotes)
                .UsingEntity<QuoteCareStaff>(

                    qc => qc.HasOne(prop => prop.CareStaff)
                    .WithMany()
                    .HasForeignKey(prop => prop.IdCareStaff),

                    qc => qc.HasOne(prop => prop.Quote)
                    .WithMany()
                    .HasForeignKey(prop => prop.IdQuote)

                );

            modelBuilder.Entity<QuoteCareStaff>()
                .HasKey(q => new { q.IdQuote, q.IdCareStaff });
        }
    }
}