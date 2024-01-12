namespace API.Dtos.ProductDtoAggregate.CategoryDtos;

public class CategoryDetailsWithSubCategoryDto
{
    public CategoryDetailsWithSubCategoryDto(CategoryDetailsDto categoryDetails, IEnumerable<CategoryDetailsDto> subCategories)
    {
        CategoryDetails = categoryDetails;
        SubCategories = subCategories;
    }

    public CategoryDetailsWithSubCategoryDto(int id, string name, int? parentId, IEnumerable<CategoryDetailsDto> subCategories)
    {
        Id = id;
        Name = name;
        ParentId = parentId;
        SubCategories = subCategories;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public CategoryDetailsDto CategoryDetails { get; set; }
    public IEnumerable<CategoryDetailsDto> SubCategories { get; set; }
}