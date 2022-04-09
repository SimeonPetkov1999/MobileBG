namespace MobileBG.Data.Seeding;
using MobileBG.Data.Seeding.SeedData.Dtos;

public class MakeModelSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.Makes.Any())
        {
            return;
        }

        var json = await File.ReadAllTextAsync(@"wwwroot/CarMakesAndModels.json");

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