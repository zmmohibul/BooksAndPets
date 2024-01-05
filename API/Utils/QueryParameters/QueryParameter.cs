namespace API.Utils.QueryParameters;

public class QueryParameter
{
    private int MaxPageSize = 24;

    public int PageNumber { get; set; } = 1;
    
    private int _pageSize = 24;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}