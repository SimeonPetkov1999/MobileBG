namespace MobileBG.Data.Seeding;

using MobileBG.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class PetrolTypeSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.PetrolTypes.Any())
        {
            return;
        }

        var json = await File.ReadAllTextAsync(@"..\..\Data\MobileBG.Data\Seeding\SeedData\PetrolTypes.json");

        var types = JsonSerializer.Deserialize<List<string>>(json);
        var entities = new List<PetrolTypeEntity>();
        foreach (var type in types)
        {
            entities.Add(new PetrolTypeEntity() { Name = type });
        }

        await dbContext.PetrolTypes.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
    }
}