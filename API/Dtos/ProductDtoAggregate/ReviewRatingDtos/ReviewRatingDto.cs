namespace API.Dtos.ProductDtoAggregate.ReviewRatingDtos;

public class ReviewRatingDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public int Rating { get; set; }
    public string Review { get; set; }
    public DateTime Date { get; set; }
}