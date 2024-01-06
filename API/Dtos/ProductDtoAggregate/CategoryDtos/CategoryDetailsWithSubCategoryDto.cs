namespace API.Dtos.ProductDtoAggregate.CategoryDtos;

public class CategoryDetailsWithSubCategoryDto
{
    public CategoryDetailsWithSubCategoryDto(CategoryDetailsDto categoryDetails, IEnumerable<CategoryDetailsDto> subCategories)
    {
        CategoryDetails = categoryDetails;
        SubCategories = subCategories;
    }

    public CategoryDetailsDto CategoryDetails { get; set; }
    public IEnumerable<CategoryDetailsDto> SubCategories { get; set; }
}