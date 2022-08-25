using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using MajorProject.Data;
using MajorProject.Models;

namespace ItemTestApixUnit
{
    public static class DbMocker
    {
        
        public static MajorProjectDbContext GetMajorProjectDbContext(string databasename)
        {
            // Create a fresh service provider for the InMemory Database instance.
            var serviceProvider = new ServiceCollection()
                                  .AddEntityFrameworkInMemoryDatabase()
                                  .BuildServiceProvider();

            // Create a new options instance,
            // telling the context to use InMemory database and the new service provider.
            var options = new DbContextOptionsBuilder<MajorProjectDbContext>()
                            .UseInMemoryDatabase(databasename)
                            .UseInternalServiceProvider(serviceProvider)
                            .Options;

            // Create the instance of the DbContext (would be an InMemory database)
            // NOTE: It will use the Scema as defined in the Data and Models folders
            var dbContext = new MajorProjectDbContext(options);

            // Add entities to the inmemory database
            dbContext.SeedData();

            return dbContext;


        }

        internal static readonly IssueCategory[] TestCollectionOfIsssues
            =
        {
            new IssueCategory
            {
                IssueCategoryId = 1,
                Issue = "Flat Tyre"
            },


            new IssueCategory
            {
                IssueCategoryId = 2,
                Issue = "Light Breaks"
            },


            new IssueCategory
            {
                IssueCategoryId = 3,
                Issue = "Clutch Issue"
            },


            new IssueCategory
            {
                IssueCategoryId = 4,
                Issue = "Last Issue"
            }
        };
        private static void SeedData(this MajorProjectDbContext context)
        {
            context.IssueCategories.AddRange(TestCollectionOfIsssues);
            context.SaveChanges();
        }

    }
}
