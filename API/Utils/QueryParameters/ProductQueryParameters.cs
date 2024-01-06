using System.Text.Json.Serialization;

namespace API.Utils.QueryParameters;

public class ProductQueryParameters : QueryParameter
{
    public int? CategoryId { get; set; }
    // public ICollection<int> CategoryIds { get; set; } = new List<int>();
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public OrderByOption OrderByOption { get; set; } = QueryParameters.OrderByOption.NameAsc;
}