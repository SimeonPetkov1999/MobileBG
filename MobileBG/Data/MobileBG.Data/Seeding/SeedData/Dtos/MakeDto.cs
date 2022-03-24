namespace MobileBG.Data.Seeding.SeedData.Dtos;
using System.Collections.Generic;

public class MakeDto
{
    public string brand { get; set; }

    public ICollection<string> models { get; set; }
}