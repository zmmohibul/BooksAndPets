namespace API.Data.SeedData;

public class BookSeed
{
    public int ProductId { get; set; }
    public string HighlightText { get; set; }
    public int PublisherId { get; set; }
    public int LanguageId { get; set; }
    public List<int> AuthorIds { get; set; } = new();
    public int PageCount { get; set; }
    public DateTime PublicationDate { get; set; }
    public string ISBN { get; set; }
    
}