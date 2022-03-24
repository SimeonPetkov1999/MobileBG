namespace MobileBG.Data.Models;

using MobileBG.Data.Common.Models;
using System;
using System.Collections.Generic;

public class ModelEntity : BaseModel<Guid>
{
    public string Name { get; set; }

    public Guid MakeId { get; set; }

    public MakeEntity Make { get; set; }

    public ICollection<CarEntity> Cars { get; set; } = new HashSet<CarEntity>();
}
