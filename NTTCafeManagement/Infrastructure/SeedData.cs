using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Ensure the database is created
                await context.Database.EnsureCreatedAsync();

                // Seed cafes only if none exist
                //if (!context.Cafes.Any())
                //{
                //    context.Cafes.AddRange(new[]
                //    {
                //    new Cafe   {Name:"Cafe C#", Description:"A C# cafe in the heart of the city", Location:"Newdelhi" }  ,
                //    new Cafe("Cafe f#", "The best f# cafe in town","Mumbai")
                     
                //});

                //    await context.SaveChangesAsync();
                //}

                //// Seed employees only if none exist
                ////if (!context.Employees.Any())
                ////{
                ////    context.Employees.AddRange(new[]
                ////    { 
                ////    new Employee("UI0000001", "Saroj Jha","srojkrjha@gmail.com", "87895654","Male" ) ,
                ////    new Employee("UI0000002", "Rita Das","rita.das@gmail.com", "89565412","Female" )
                ////});

                //    //await context.SaveChangesAsync();
                //}
            }
        }
    }
}
