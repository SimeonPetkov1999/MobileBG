namespace MobileBG.Data.Seeding;

using MobileBG.Data.Models;
using MobileBG.Data.Seeding.SeedData.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class MakeModelSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.Makes.Any())
        {
            return;
        }

        var json = await File.ReadAllTextAsync(@"..\..\Data\MobileBG.Data\Seeding\SeedData\CarMakesAndModels.json");

        var model = JsonSerializer.Deserialize<List<MakeDto>>(json);
        var makes = new List<MakeEntity>();
        foreach (var currentMake in model)
        {
            var entity = new MakeEntity() { Name = currentMake.brand };
            foreach (var currnentModel in currentMake.models)
            {
                entity.Models.Add(new ModelEntity() { Name = currnentModel });
            }

            makes.Add(entity);
        }

        await dbContext.Makes.AddRangeAsync(makes);
        await dbContext.SaveChangesAsync();
    }
}