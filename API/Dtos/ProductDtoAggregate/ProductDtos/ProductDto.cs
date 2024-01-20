using API.Dtos.ProductDtoAggregate.CategoryDtos;
using API.Dtos.ProductDtoAggregate.DepartmentDtos;

namespace API.Dtos.ProductDtoAggregate.ProductDtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DepartmentDto Department { get; set; }
    public ICollection<CategoryDetailsDto> Categories { get; set; }
    public ICollection<ProductPictureDto> Pictures { get; set; }
    public ICollection<PriceDto> PriceList { get; set; }
    public double AverageRating { get; set; }
    public int RatingCount { get; set; }
}