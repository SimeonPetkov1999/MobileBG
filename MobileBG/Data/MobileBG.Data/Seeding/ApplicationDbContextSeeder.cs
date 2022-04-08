namespace MobileBG.Data.Seeding;

using Microsoft.Extensions.Logging;

public class ApplicationDbContextSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }

        if (serviceProvider == null)
        {
            throw new ArgumentNullException(nameof(serviceProvider));
        }

        var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(ApplicationDbContextSeeder));

        var seeders = new List<ISeeder>
                          {
                              new RolesSeeder(),
                              new UserSeeder(),
                              new MakeModelSeeder(),
                              new CitiesSeeder(),
                              new PetrolTypeSeeder(),
                              new CarSeeder(),
                          };

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(dbContext, serviceProvider);
            await dbContext.SaveChangesAsync();
            logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
        }
    }
}