using API.Entities;
using API.Entities.BaseEntities;
using API.Entities.BookAggregate;
using API.Entities.Identity;
using API.Entities.OrderAggregate;
using API.Entities.ProductAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : IdentityDbContext<User>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> ProductCategories { get; set; }
    public DbSet<Department> ProductDepartments { get; set; }
    public DbSet<MeasureType> ProductMeasureTypes { get; set; }
    public DbSet<MeasureOption> ProductMeasureOptions { get; set; }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Language> Languages { get; set; }
    
    public DbSet<Picture> ProductPictures { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasOne(product => product.Department)
            .WithMany(dep => dep.Products)
            .HasForeignKey(prod => prod.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Order>()
            .Property(o => o.OrderStatus)
            .HasConversion<string>();

        modelBuilder.Entity<Department>()
            .HasMany(dep => dep.Products)
            .WithOne(prod => prod.Department)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Price>()
            .HasOne(p => p.MeasureType)
            .WithMany(m => m.Prices)
            .HasForeignKey(p => p.MeasureTypeId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Price>()
            .HasOne(p => p.MeasureOption)
            .WithMany(m => m.Prices)
            .HasForeignKey(p => p.MeasureOptionId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Category>()
            .HasOne(s => s.Parent)
            .WithMany(m => m.Children)
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}