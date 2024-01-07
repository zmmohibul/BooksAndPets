using System.Text.Json.Serialization;

namespace API.Utils.QueryParameters;

public class ProductQueryParameters : QueryParameter
{
    public int? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public ProductOrderByOption? ProductOrderByOption { get; set; }
}