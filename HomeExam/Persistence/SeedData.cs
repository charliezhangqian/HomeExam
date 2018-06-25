using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeExam.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HomeExam.Persistence
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ExamDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ExamDbContext>>()))
            {
                // Look for any movies.
                if (context.Contacts.Any() || context.Projects.Any())
                {
                    return;   // DB has been seeded
                }

                context.Projects.AddRange(
                    new Project() { Name = "Project1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2) },
                    new Project() { Name = "Project2", StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1) },
                    new Project() { Name = "Project3", StartDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(3) }
                );

                context.Contacts.AddRange(
                    new Contact() { Name = "charlie", Email = "charlie@charlie.com", Phone = "123" },
                    new Contact() { Name = "david", Email = "david@david.com", Phone = "1234" },
                    new Contact() { Name = "susan", Email = "susan@susan.com", Phone = "1235" }
                );
                context.SaveChanges();
            }
        }
    }
}
