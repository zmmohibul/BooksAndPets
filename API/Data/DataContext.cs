using API.Entities.BookAggregate;
using API.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> ProductCategories { get; set; }
    public DbSet<MeasureType> ProductMeasureTypes { get; set; }
    public DbSet<MeasureOption> ProductMeasureOptions { get; set; }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Language> Languages { get; set; }
    
    public DbSet<Picture> ProductPictures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Price>()
            .HasOne(p => p.MeasureType)
            .WithMany(m => m.Prices)
            .HasForeignKey(p => p.MeasureTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Price>()
            .HasOne(p => p.MeasureOption)
            .WithMany(m => m.Prices)
            .HasForeignKey(p => p.MeasureOptionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Category>()
            .HasOne(s => s.Parent)
            .WithMany(m => m.Children)
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}