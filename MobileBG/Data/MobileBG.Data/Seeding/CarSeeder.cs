namespace MobileBG.Data.Seeding;

using MobileBG.Data.Seeding.SeedData.Dtos;

public class CarSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.Cars.Any())
        {
            return;
        }

        var json = await File.ReadAllTextAsync(@"wwwroot/Cars.json");

        var cars = JsonSerializer.Deserialize<List<CarDto>>(json);
        var carsEntities = new List<CarEntity>();
        var userId = dbContext.Users.FirstOrDefault(x => x.Email == "simeon99@abv.bg").Id;

        foreach (var currentCar in cars)
        {
            var car = new CarEntity()
            {
                MakeId = dbContext.Makes.First(x => x.Name == currentCar.Make).Id,
                ModelId = dbContext.Models.First(x => x.Name == currentCar.Model).Id,
                CityId = dbContext.Cities.First(x => x.Name == currentCar.City).Id,
                PetrolTypeId = dbContext.PetrolTypes.First(x => x.Name == currentCar.PetrolType).Id,
                Description = currentCar.Description,
                HorsePower = currentCar.HorsePower,
                Km = currentCar.Km,
                Price = currentCar.Price,
                IsApproved = true,
                CreatedOn = DateTime.Now,
                YearMade = currentCar.YearMade,
                UserId = userId,
            };

            foreach (var link in currentCar.ImageLinks)
            {
                car.Images.Add(new ImageEntity() { ImageUrl = link });
            }

            carsEntities.Add(car);
        }

        await dbContext.Cars.AddRangeAsync(carsEntities);
        await dbContext.SaveChangesAsync();
    }
}
