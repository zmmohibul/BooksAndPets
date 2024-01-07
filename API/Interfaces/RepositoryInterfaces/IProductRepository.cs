using API.Entities.BaseEntities;
using API.Utils.QueryParameters;

namespace API.Interfaces.RepositoryInterfaces;

public interface IProductRepository
{
    public IQueryable<T> ApplyProductFilters<T>(IQueryable<T> queryable, ProductQueryParameters parameters)
        where T : BaseProduct;
}