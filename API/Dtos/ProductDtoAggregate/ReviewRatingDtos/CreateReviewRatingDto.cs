using System.ComponentModel.DataAnnotations;

namespace API.Dtos.ProductDtoAggregate.ReviewRatingDtos;

public class CreateReviewRatingDto
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int Rating { get; set; }
    public string? Review { get; set; }
}