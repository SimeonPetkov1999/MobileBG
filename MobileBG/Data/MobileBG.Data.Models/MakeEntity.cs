namespace MobileBG.Data.Models;

public class MakeEntity : BaseModel<Guid>
{
    public string Name { get; set; }

    public ICollection<CarEntity> Cars { get; set; } = new HashSet<CarEntity>();

    public ICollection<ModelEntity> Models { get; set; } = new HashSet<ModelEntity>();
}
