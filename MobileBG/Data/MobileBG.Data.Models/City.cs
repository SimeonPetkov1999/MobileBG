namespace MobileBG.Data.Models;

using MobileBG.Data.Common.Models;
using System;
using System.Collections.Generic;

public class City : BaseModel<Guid>
{
    public string Name { get; set; }

    public ICollection<Car> Cars { get; set; } = new HashSet<Car>();
}
