using System.ComponentModel.DataAnnotations;
using API.Dtos.ProductDtoAggregate.ProductDtos;

namespace API.Dtos.BookDtoAggregate.BookDtos;

public class CreateBookDto : CreateProductDto
{
    [Required]
    public string HighlightText { get; set; }

    [Required]
    public int PublisherId { get; set; }
    
    [Required]
    public int LanguageId { get; set; }
    
    [Required]
    public ICollection<int> AuthorIds { get; set; }
    
    [Required]
    public int PageCount { get; set; }
    
    [Required]
    public DateTime PublicationDate { get; set; }
    
    [Required]
    public string ISBN { get; set; }
}