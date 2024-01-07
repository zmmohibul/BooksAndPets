using API.Dtos.ProductDtoAggregate.ProductDtos;
using API.Entities.BaseEntities;
using API.Entities.ProductAggregate;
using API.Utils;
using API.Utils.QueryParameters;

namespace API.Interfaces.RepositoryInterfaces;

public interface IProductRepository
{
    public Task<Result<Product>> CreateProduct(CreateProductDto createProductDto);
    public IQueryable<T> ApplyProductFilters<T>(IQueryable<T> queryable, ProductQueryParameters parameters)
        where T : BaseProduct;
}