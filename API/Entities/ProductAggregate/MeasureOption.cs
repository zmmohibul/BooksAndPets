using API.Entities.BaseEntities;

namespace API.Entities.ProductAggregate;

public class MeasureOption : BaseEntityWithDescription
{
    public string Option { get; set; }

    public int MeasureTypeId { get; set; }
    public MeasureType MeasureType { get; set; }
    public ICollection<Price> Prices { get; set; }
}