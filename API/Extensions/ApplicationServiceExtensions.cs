using API.Data;
using API.Data.Repositories.Book;
using API.Data.Repositories.Product;
using API.Interfaces;
using API.Interfaces.RepositoryInterfaces.Book;
using API.Interfaces.RepositoryInterfaces.Product;
using API.Services;
using API.Utils;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
        
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IMeasureTypeRepository, MeasureTypeRepository>();
        services.AddScoped<IMeasureOptionRepository, MeasureOptionRepository>();
        
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddScoped<IPictureUploadService, PictureUploadService>();
        
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        return services;
    }
}