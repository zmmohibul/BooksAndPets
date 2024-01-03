namespace API.Dtos.Product.Category;

public class CategoryDetailsDto
{
    public CategoryDetailsDto() { }

    public CategoryDetailsDto(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}