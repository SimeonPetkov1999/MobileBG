namespace MobileBG.Data.Models;

public class ModelEntity : BaseModel<Guid>
{
    public string Name { get; set; }

    public Guid MakeId { get; set; }

    public MakeEntity Make { get; set; }

    public ICollection<CarEntity> Cars { get; set; } = new HashSet<CarEntity>();
}
