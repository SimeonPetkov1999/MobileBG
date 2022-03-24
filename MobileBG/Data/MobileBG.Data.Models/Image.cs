namespace MobileBG.Data.Models;
using MobileBG.Data.Common.Models;
using System;


public class Image : BaseModel<Guid>
{
    public string ImageUrl { get; set; }

    public Guid CarId { get; set; }

    public Car Car { get; set; }

}
