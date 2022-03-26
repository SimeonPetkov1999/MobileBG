namespace MobileBG.Data.Models;

public class CityEntity : BaseModel<Guid>
{
    public string Name { get; set; }

    public ICollection<CarEntity> Cars { get; set; } = new HashSet<CarEntity>();
}
