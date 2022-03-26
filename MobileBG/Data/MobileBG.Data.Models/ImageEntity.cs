namespace MobileBG.Data.Models;

public class ImageEntity : BaseModel<Guid>
{
    public string ImageUrl { get; set; }

    public Guid CarId { get; set; }

    public CarEntity Car { get; set; }
}