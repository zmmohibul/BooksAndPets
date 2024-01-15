using System.Reflection;
using System.Text.Json;
using API.Data.SeedData;
using API.Entities.BookAggregate;
using API.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContextSeed
{
    public static async Task SeedAsync(DataContext context)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!context.Products.Any())
        {
            // products
            var departmentsData = File.ReadAllText(path + @"/Data/SeedData/departments.json");
            var departments = JsonSerializer.Deserialize<List<Department>>(departmentsData);
            context.ProductDepartments.AddRange(departments);
            
            var categoriesData = File.ReadAllText(path + @"/Data/SeedData/categories.json");
            var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
            context.ProductCategories.AddRange(categories);
            
            var measureTypesData = File.ReadAllText(path + @"/Data/SeedData/measureTypes.json");
            var measureTypes = JsonSerializer.Deserialize<List<MeasureType>>(measureTypesData);
            context.ProductMeasureTypes.AddRange(measureTypes);
            
            var measureOptionsData = File.ReadAllText(path + @"/Data/SeedData/measureOptions.json");
            var measureOptions = JsonSerializer.Deserialize<List<MeasureOption>>(measureOptionsData);
            context.ProductMeasureOptions.AddRange(measureOptions);
            
            var productsData = File.ReadAllText(path + @"/Data/SeedData/products.json");
            var productSeeds = JsonSerializer.Deserialize<List<ProductSeed>>(productsData);
            foreach (var p in productSeeds)
            {
                var product = new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    DepartmentId = p.DepartmentId,
                    Categories = new List<Category>(),
                    PriceList = p.PriceList,
                    Pictures = p.Pictures
                };

                foreach (var c in p.CategoryIds)
                {
                    product.Categories.Add(categories.First(cat => cat.Id == c));
                }

                context.Products.Add(product);
            }


            // books
            var authorsData = File.ReadAllText(path + @"/Data/SeedData/authors.json");
            var authors = JsonSerializer.Deserialize<List<Author>>(authorsData);
            context.Authors.AddRange(authors);
            
            var publishersData = File.ReadAllText(path + @"/Data/SeedData/publishers.json");
            var publishers = JsonSerializer.Deserialize<List<Publisher>>(publishersData);
            context.Publishers.AddRange(publishers);
            
            var languagesData = File.ReadAllText(path + @"/Data/SeedData/languages.json");
            var languages = JsonSerializer.Deserialize<List<Language>>(languagesData);
            context.Languages.AddRange(languages);
            
            var booksData = File.ReadAllText(path + @"/Data/SeedData/books.json");
            var bookSeeds = JsonSerializer.Deserialize<List<BookSeed>>(booksData);
            foreach (var b in bookSeeds)
            {
                var book = new Book
                {
                    ProductId = b.ProductId,
                    HighlightText = b.HighlightText,
                    PublisherId = b.PublisherId,
                    LanguageId = b.LanguageId,
                    PageCount = b.PageCount,
                    PublicationDate = DateTime.SpecifyKind(b.PublicationDate, DateTimeKind.Utc),
                    Authors = new List<Author>(),
                    ISBN = b.ISBN
                };

                foreach (var authorId in b.AuthorIds)
                {
                    book.Authors.Add(authors.First(a => a.Id == authorId));
                }

                context.Books.Add(book);
            }
        }


        if (context.ChangeTracker.HasChanges())
        {
            await context.SaveChangesAsync();
        }
    }
}