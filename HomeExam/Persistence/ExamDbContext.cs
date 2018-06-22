using HomeExam.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeExam.Persistence
{
    public class ExamDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Project>().Property(p => p.StartDate).IsRequired();
            modelBuilder.Entity<Project>().Property(p => p.EndDate).IsRequired();

            modelBuilder.Entity<Contact>().ToTable("Contacts");
            modelBuilder.Entity<Contact>().Property(c => c.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Contact>().Property(c => c.Phone).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Contact>().Property(c => c.Email).HasMaxLength(255);

            modelBuilder.Entity<ProjectContact>().ToTable("ProjectContacts");
            modelBuilder.Entity<ProjectContact>().HasKey(pc =>
                new { pc.ProjectId, pc.ContactId });
        }

    }
}
