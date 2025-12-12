using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nabd.Core.Interfaces; 

namespace Nabd.Shared.Extensions
{
    public static class DatabaseSeederExtension
    {
    
        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                
                var seeder = services.GetService<IDbSeeder>();

                if (seeder != null)
                {
                    await seeder.SeedAsync();
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw; 
            }
        }

        public static async Task ClearDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while clearing the database: {ex.Message}");
                throw;
            }
        }
    }
}