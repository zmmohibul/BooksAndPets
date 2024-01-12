using System.Reflection;
using System.Text.Json;
using API.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContextSeed
{
    public static async Task SeedAsync(DataContext context)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        
        if (!context.ProductDepartments.Any())
        {
            var departmentsData = File.ReadAllText(path + @"/Data/SeedData/departments.json");
            var departments = JsonSerializer.Deserialize<List<Department>>(departmentsData);
            context.ProductDepartments.AddRange(departments);
        }
        
        if (!context.ProductCategories.Any())
        {
            var categoriesData = File.ReadAllText(path + @"/Data/SeedData/categories.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
            context.ProductCategories.AddRange(categories);
        }



        if (context.ChangeTracker.HasChanges())
        {
            // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductDepartments ON");
            // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductCategories ON");
            await context.SaveChangesAsync();
            // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductDepartments OFF");
            // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductCategories OFF");
        }
        
        // await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductDepartments OFF");
        // await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ProductCategories OFF");
    }
}