namespace MobileBG.Data.Models;

public class CarEntity : BaseModel<Guid>
{
    public Guid MakeId { get; set; }

    public MakeEntity Make { get; set; }

    public Guid ModelId { get; set; }

    public ModelEntity Model { get; set; }

    public Guid PetrolTypeId { get; set; }

    public PetrolTypeEntity PetrolType { get; set; }

    public Guid CityId { get; set; }

    public CityEntity City { get; set; }

    public decimal Price { get; set; }

    public int YearMade { get; set; }

    public int Km { get; set; }

    public int HorsePower { get; set; }

    public string Description { get; set; }

    public string UserId { get; set; }

    public bool IsApproved { get; set; } = false;

    public ApplicationUser User { get; set; }

    public ICollection<ImageEntity> Images { get; set; } = new HashSet<ImageEntity>();
}
