namespace MobileBG.Data.Seeding;

public class PetrolTypeSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.PetrolTypes.Any())
        {
            return;
        }

        var json = await File.ReadAllTextAsync(@"wwwroot/PetrolTypes.json");

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