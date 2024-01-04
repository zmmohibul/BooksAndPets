using API.Entities.BaseEntities;

namespace API.Entities.ProductAggregate;

public class MeasureType : BaseEntityWithDescription
{
    // { "type": "book", "measureOptions": [ "paperback", "hardcover" ] }
    // { "type": "weight", "measureOptions": [ "3 kg", "7 kg", "10 kg" ] }
    public string Type { get; set; }

    public ICollection<MeasureOption> MeasureOptions { get; set; } = new List<MeasureOption>();
    public ICollection<Price> Prices { get; set; } = new List<Price>();
}