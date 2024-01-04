using API.Data;
using API.Data.Repositories.Product;
using API.Interfaces;
using API.Interfaces.RepositoryInterfaces.Product;
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
        
        return services;
    }
}