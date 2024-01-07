using API.Entities.BaseEntities;
using API.Entities.BookAggregate;
using API.Interfaces.RepositoryInterfaces;
using API.Utils.QueryParameters;

namespace API.Data.Repositories;

public class ProductRepository : IProductRepository
{
    public IQueryable<T> ApplyProductFilters<T>(IQueryable<T> queryable, ProductQueryParameters parameters) where T : BaseProduct
    {
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

        return queryable.AsQueryable();
    }
}