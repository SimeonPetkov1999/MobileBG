namespace MobileBG.Services.Data;

using MobileBG.Web.ViewModels.Cities;

public class CityService : ICityService
{
    private readonly IRepository<CityEntity> cityRepo;

    public CityService(IRepository<CityEntity> cityRepo)
    {
        this.cityRepo = cityRepo;
    }

    public async Task<Guid> CreateCityAsync(string cityName)
    {
        var city = new CityEntity() { Name = cityName, Id = Guid.NewGuid() };
        await this.cityRepo.AddAsync(city);
        await this.cityRepo.SaveChangesAsync();

        return city.Id;
    }

    public async Task<bool> DeleteCityAsync(Guid cityId)
    {
        var city = await this.cityRepo
            .All()
            .Include(x => x.Cars)
            .Where(x => x.Id == cityId)
            .FirstOrDefaultAsync();

        if (city != null && city.Cars.Any())
        {
            return false;
        }

        this.cityRepo.Delete(city);
        await this.cityRepo.SaveChangesAsync();
        return true;
    }

    public async Task<ICollection<CityInfoViewModel>> GetAllCitiesAsync(string keyWord)
    {
        var query = this.cityRepo
            .AllAsNoTracking();

        if (keyWord != null)
        {
            query = query.Where(x => x.Name.ToLower().Contains(keyWord.ToLower()));
        }

        var cities = await query
            .To<CityInfoViewModel>()
            .ToListAsync();

        return cities;
    }
}
