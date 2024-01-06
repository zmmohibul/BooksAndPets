namespace API.Utils.QueryParameters;

public class QueryParameter
{
    private int _pageNumber = 1;
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = (value < 1) ? _pageNumber : value;
    }
    
    private int _pageSize = 24;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > 24) ? 24 : value;
    }

    public string SearchTerm { get; set; } = "";
}