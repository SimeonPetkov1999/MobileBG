namespace MobileBG.Data.Seeding;

using MobileBG.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class CitiesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.Cities.Any())
        {
            return;
        }

        var json = await File.ReadAllTextAsync(@"..\..\Data\MobileBG.Data\Seeding\SeedData\Cities.json");

        var cities = JsonSerializer.Deserialize<List<string>>(json);
        var entities = new List<CityEntity>();
        foreach (var city in cities)
        {
            entities.Add(new CityEntity() { Name = city });
        }

        await dbContext.Cities.AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
    }
}
