namespace MobileBG.Data.Models;
using MobileBG.Data.Common.Models;
using System;

public class ImageEntity : BaseModel<Guid>
{
    public string ImageUrl { get; set; }

    public Guid CarId { get; set; }

    public CarEntity Car { get; set; }
}