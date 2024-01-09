using System.Text;
using API.Dtos.ProductDtoAggregate.ProductDtos;
using API.Entities.BaseEntities;
using API.Entities.BookAggregate;
using API.Entities.ProductAggregate;
using API.Interfaces.RepositoryInterfaces;
using API.Utils;
using API.Utils.QueryParameters;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Result<Product>> CreateProduct(CreateProductDto createProductDto)
    {
        var product = new Product();
        var valResult = await ValidateIdsInCreateProductDto(product, createProductDto);
        return valResult.StatusCode != 200 
            ? new Result<Product>(valResult.StatusCode, valResult.Message) 
            : new Result<Product>(200, product);
    }

    public async Task<Result<Product>> ValidateIdsInCreateProductDto(Product product, CreateProductDto createProductDto)
    {
        var department = await _context.ProductDepartments
            .FirstOrDefaultAsync(pd => pd.Id == createProductDto.DepartmentId);
        if (department == null)
        {
            return new Result<Product>(400, $"Department id: {createProductDto.DepartmentId} does not exist");
        }

        var categories = await _context.ProductCategories
            .Where(pc => createProductDto.CategoryIds.Contains(pc.Id))
            .ToListAsync();
        foreach (var category in categories.Where(category => category.DepartmentId != createProductDto.DepartmentId))
        {
            return new Result<Product>(400,
                $"Category id: {category.Id} does not belong to Department id: {createProductDto.DepartmentId}");
        }

        var categoryIds = categories.Select(category => category.Id).ToHashSet();
        var notFoundCategoryIds = new StringBuilder();
        foreach (var id in createProductDto.CategoryIds)
        {
            if (!categoryIds.Contains(id))
            {
                notFoundCategoryIds.Append($"{id}, ");
            }
        }

        if (notFoundCategoryIds.Length > 0)
        {
            notFoundCategoryIds.Remove(notFoundCategoryIds.Length - 2, 2);
            return new Result<Product>(400, $"Category id: {notFoundCategoryIds} does not exist");
        }

        var priceList = new List<Price>();
        foreach (var price in createProductDto.PriceList)
        {
            var measureType = await _context.ProductMeasureTypes.FirstOrDefaultAsync(mt => mt.Id == price.MeasureTypeId);
            if (measureType == null)
            {
                return new Result<Product>(400, $"Measure Type id: {price.MeasureTypeId} does not exist.");
            }

            var measureOption =
                await _context.ProductMeasureOptions.FirstOrDefaultAsync(pmo => pmo.Id == price.MeasureOptionId);
            if (measureOption == null)
            {
                return new Result<Product>(400, $"Measure Option id: {price.MeasureOptionId} does not exist.");
            }

            if (measureOption.MeasureTypeId != price.MeasureTypeId)
            {
                return new Result<Product>(400, $"Measure Option id: {price.MeasureOptionId} does not belong to Measure Type id: {price.MeasureTypeId}");
            }
            
            priceList.Add(new Price()
            {
                UnitPrice = price.UnitPrice,
                MeasureType = measureType,
                MeasureOption = measureOption,
                QuantityInStock = price.QuantityInStock
            });
        }

        product.Name = createProductDto.Name.ToLower();
        product.Description = createProductDto.Description;
        product.Department = department;
        product.Categories = categories;
        product.PriceList = priceList;

        return new Result<Product>(200, product);
    }
    
    public IQueryable<T> GetFilteredQueryable<T>(ProductQueryParameters parameters) where T : BaseProduct
    {
        var queryable = _context.Set<T>().AsQueryable();
        if (parameters.CategoryId != null)
        {
            queryable = queryable.Where((baseProd => baseProd.Product.Categories.Any(pc => pc.Id == parameters.CategoryId)));
        }
        
        if (parameters.MinPrice != null)
        {
            queryable = queryable.Where(baseProd =>
                baseProd.Product.PriceList.Select(p => p.UnitPrice).Min() >= parameters.MinPrice
                || baseProd.Product.PriceList.Select(p => p.UnitPrice).Max() >= parameters.MinPrice);
        }
        
        if (parameters.MaxPrice != null)
        {
            queryable = queryable.Where(baseProd =>
                baseProd.Product.PriceList.Select(p => p.UnitPrice).Min() <= parameters.MaxPrice
                || baseProd.Product.PriceList.Select(p => p.UnitPrice).Max() <= parameters.MaxPrice);
        }

        if (parameters.ProductOrderByOption != null)
        {
            queryable = parameters.ProductOrderByOption switch
            {
                ProductOrderByOption.NameAsc => queryable.OrderBy(book => book.Product.Name),
                ProductOrderByOption.NameDesc => queryable.OrderByDescending(book => book.Product.Name),
                ProductOrderByOption.PriceAsc => queryable.OrderBy(book => book.Product.PriceList.Min(p => p.UnitPrice)),
                ProductOrderByOption.PriceDesc => queryable.OrderByDescending(book => book.Product.PriceList.Min(p => p.UnitPrice)),
                _ => queryable.OrderBy(book => book.Product.Name)
            };
        }
        else
        {
            queryable = queryable.OrderBy(book => book.Product.Name);
        }

        return queryable;
    }
}