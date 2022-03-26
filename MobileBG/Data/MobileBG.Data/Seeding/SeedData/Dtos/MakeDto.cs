namespace MobileBG.Data.Seeding.SeedData.Dtos;
using System.Collections.Generic;

public class MakeDto
{
#pragma warning disable SA1300 // Element should begin with upper-case letter
    public string brand { get; set; }

    public ICollection<string> models { get; set; }
#pragma warning restore SA1300 // Element should begin with upper-case letter
}