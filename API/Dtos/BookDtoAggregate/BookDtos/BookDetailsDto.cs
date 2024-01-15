using API.Dtos.BookDtoAggregate.AuthorDtos;
using API.Dtos.BookDtoAggregate.LanguageDtos;
using API.Dtos.BookDtoAggregate.PublisherDtos;
using API.Dtos.ProductDtoAggregate.ProductDtos;

namespace API.Dtos.BookDtoAggregate.BookDtos;

public class BookDetailsDto : ProductDto
{
    public string HighlightText { get; set; }
    
    public PublisherDto Publisher { get; set; }
    public ICollection<AuthorDto> Authors { get; set; }
    
    public LanguageDto Language { get; set; }
    
    public int PageCount { get; set; }
    public DateTime PublicationDate { get; set; }
    public string ISBN { get; set; }
}