namespace MobileBG.Data.Models;

using MobileBG.Data.Common.Models;
using System;
using System.Collections.Generic;

public class CityEntity : BaseModel<Guid>
{
    public string Name { get; set; }

    public ICollection<CarEntity> Cars { get; set; } = new HashSet<CarEntity>();
}
