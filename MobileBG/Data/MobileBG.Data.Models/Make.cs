namespace MobileBG.Data.Models;
using MobileBG.Data.Common.Models;
using System;
using System.Collections.Generic;

public class Make : BaseModel<Guid>
{
    public string Name { get; set; }

    public ICollection<Car> Cars { get; set; } = new HashSet<Car>();

    public ICollection<Model> Models { get; set; } = new HashSet<Model>();
}
