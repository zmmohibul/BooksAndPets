namespace API.Dtos.BookDtoAggregate.BookDtos;

public class UpdateBookDto
{
    public string HighlightText { get; set; }

    public int? PublisherId { get; set; }
    
    public int? LanguageId { get; set; }
    
    public ICollection<int>? AuthorIds { get; set; }
    
    public int PageCount { get; set; }
    
    public DateTime PublicationDate { get; set; }
    
    public string ISBN { get; set; }
}