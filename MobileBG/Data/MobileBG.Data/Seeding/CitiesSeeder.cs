namespace MobileBG.Data.Seeding;

public class CitiesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.Cities.Any())
        {
            return;
        }

        var json = await File.ReadAllTextAsync(@"wwwroot/Cities.json");

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
