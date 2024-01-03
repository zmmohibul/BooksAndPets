namespace API.Dtos.Product.Category;

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