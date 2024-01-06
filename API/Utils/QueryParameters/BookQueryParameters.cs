namespace API.Utils.QueryParameters;

public class BookQueryParameters : ProductQueryParameters
{
    public ICollection<int>? AuthorIds { get; set; }
    public ICollection<int>? PublisherIds { get; set; }
    public ICollection<int>? LanguageIds { get; set; }
}