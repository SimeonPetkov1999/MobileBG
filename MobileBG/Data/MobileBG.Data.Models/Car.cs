namespace MobileBG.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MobileBG.Data.Common.Models;

public class Car : BaseModel<Guid>
{
    public Guid MakeId { get; set; }

    public Make Make { get; set; }

    public Guid ModelId { get; set; }

    public Model Model { get; set; }

    public Guid PetrolTypeId { get; set; }

    public PetrolType PetrolType { get; set; }

    public Guid CityId { get; set; }

    public City City { get; set; }

    public decimal Price { get; set; }

    public int YearMade { get; set; }

    public int HorsePower { get; set; }

    public string Description { get; set; }

    public string UserId { get; set; }

    public bool IsApproved { get; set; } = false;

    public ApplicationUser User { get; set; }

    public ICollection<Image> Images { get; set; } = new HashSet<Image>();
}
