using API.Dtos.BookDtoAggregate.AuthorDtos;
using API.Dtos.BookDtoAggregate.LanguageDtos;
using API.Dtos.BookDtoAggregate.PublisherDtos;
using API.Dtos.ProductDtoAggregate.ProductDtos;

namespace API.Dtos.BookDtoAggregate.BookDtos;

public class BookDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string MainPictureUrl { get; set; }
    public ICollection<PriceDto> PriceList { get; set; }
    public string HighlightText { get; set; }
    public ICollection<AuthorDto> Authors { get; set; }
    public DateTime PublicationDate { get; set; }
    public double AverageRating { get; set; }
    public int RatingCount { get; set; }
}