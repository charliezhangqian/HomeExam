using HomeExam.Core.Models;
using HomeExam.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace HomeExam.Test.Helper
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ExamDbContext db)
        {
            db.Projects.AddRange(GetSeedingProjects());
            db.Contacts.AddRange(GetSeedingContacts());
            db.SaveChanges();
        }

        private static IEnumerable<Project> GetSeedingProjects()
        {
            return new List<Project>()
            {
                new Project(){ Name="Project1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2)},
                new Project(){ Name="Project2", StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now.AddDays(1)},
                new Project(){ Name="Project3", StartDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(3)}
            };
        }

        private static IEnumerable<Contact> GetSeedingContacts()
        {
            return new List<Contact>()
            {
                new Contact(){Name = "charlie", Email = "charlie@charlie.com", Phone = "123"},
                new Contact(){Name = "david", Email = "david@david.com", Phone = "1234"},
                new Contact(){Name = "susan", Email = "susan@susan.com", Phone = "1235"},
            };
        }

        public static WebApplicationFactory<Startup> BuildWebAppFactory(WebApplicationFactory<Startup> webAppFactory)
        {
            return webAppFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Create a new service provider.
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    // Add a database context (AppDbContext) using an in-memory 
                    // database for testing.
                    services.AddDbContext<ExamDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTests");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    // Build the service provider.
                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    // context (AppDbContext).
                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<ExamDbContext>();

                        // Ensure the database is created.
                        db.Database.EnsureCreated();

                        // Seed the database with test data.
                        Utilities.InitializeDbForTests(db);
                    }
                });
            });
        }
    }
}