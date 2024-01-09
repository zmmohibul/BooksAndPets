using API.Dtos.ProductDtoAggregate.ProductDtos;
using API.Entities.BaseEntities;
using API.Entities.ProductAggregate;
using API.Utils;
using API.Utils.QueryParameters;

namespace API.Interfaces.RepositoryInterfaces;

public interface IProductRepository
{
    public Task<Result<Product>> CreateProduct(CreateProductDto createProductDto);
    public Task<Result<Product>> ValidateIdsInCreateProductDto(Product product, CreateProductDto createProductDto);
    public IQueryable<T> GetFilteredQueryable<T>(ProductQueryParameters parameters) where T : BaseProduct;
}